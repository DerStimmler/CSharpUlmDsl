using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using UlmDslClient.Models;
using UlmDslClient.Utils;

namespace UlmDslClient;

public static class UlmDslClient
{
  public static IReadOnlyList<UlmDslMail> GetMails(string name)
  {
    var mails = new List<UlmDslMail>();

    var mailInfos = GetMailInfos(name);

    foreach (var mailInfo in mailInfos)
    {
      var feed = UlmDslService.FetchMailFeed(name, mailInfo.Id);

      var mail = feed.Items.First();

      mails.Add(new UlmDslMail
      {
        Id = Convert.ToInt32(mail.Id),
        Link = mail.Links.First().Uri,
        Subject = mail.Title.Text,
        Date = mailInfo.Date,
        Recipient = mailInfo.Recipient,
        Sender = mailInfo.Sender,
        Body = ExtractBodyFromContent(mail.Content)
      });
    }

    return mails.AsReadOnly();
  }

  public static UlmDslMail GetMailById(string name, int id)
  {
    var mailInfos = GetMailInfos(name);

    var mailInfo = mailInfos.SingleOrDefault(mail => mail.Id == id);

    if (mailInfo is null)
      throw new ArgumentException($"There is no mail with id {id}");

    var feed = UlmDslService.FetchMailFeed(name, id);

    var mail = feed.Items.First();

    return new UlmDslMail
    {
      Id = Convert.ToInt32(mail.Id),
      Link = mail.Links.First().Uri,
      Subject = mail.Title.Text,
      Date = mailInfo.Date,
      Recipient = mailInfo.Recipient,
      Sender = mailInfo.Sender,
      Body = ExtractBodyFromContent(mail.Content)
    };
  }

  public static IReadOnlyList<UlmDslMailInfo> GetMailInfos(string name)
  {
    var feed = UlmDslService.FetchInboxFeed(name);

    return feed.Items
      .Select(item => new UlmDslMailInfo
      {
        Id = Convert.ToInt32(item.Id),
        Link = item.Links.First().Uri,
        Subject = item.Title.Text,
        Date = ExtractDateFromSummary(item.Summary.Text),
        Recipient = ExtractRecipientFromSummary(item.Summary.Text),
        Sender = ExtractSenderFromSummary(item.Summary.Text)
      })
      .ToList()
      .AsReadOnly();
  }

  private static UlmDslMailRecipient ExtractRecipientFromSummary(string summary)
  {
    var regex = new Regex("to => (?<DisplayName>.+) <(?<Email>.+)>");
    var match = regex.Match(summary);

    var recipientDisplayNameEncoded = match.Groups["DisplayName"].Value;
    var recipientDisplayName = StringUtils.DecodeQuotedPrintable(recipientDisplayNameEncoded);
    var recipientEmail = match.Groups["Email"].Value;

    return new UlmDslMailRecipient
    {
      DisplayName = recipientDisplayName,
      Email = recipientEmail
    };
  }

  private static string ExtractBodyFromContent(SyndicationContent content) =>
    ((TextSyndicationContent) content).Text.Trim();

  private static UlmDslMailSender ExtractSenderFromSummary(string summary)
  {
    var regex = new Regex("from => (?<DisplayName>.+) <(?<Email>.+)>");
    var match = regex.Match(summary);

    var senderDisplayNameEncoded = match.Groups["DisplayName"].Value;
    var senderDisplayName = StringUtils.DecodeQuotedPrintable(senderDisplayNameEncoded);
    var senderEmail = match.Groups["Email"].Value;

    return new UlmDslMailSender
    {
      DisplayName = senderDisplayName,
      Email = senderEmail
    };
  }

  private static DateTimeOffset ExtractDateFromSummary(string summary)
  {
    var regex = new Regex("date => (?<Date>.+)\n");
    var match = regex.Match(summary);

    return DateTimeOffset.Parse(match.Groups["Date"].Value);
  }
}
