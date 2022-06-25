using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using UlmDslClient.Models;
using UlmDslClient.Utils;

namespace UlmDslClient;

public class UlmDslClient
{
  private readonly UlmDslService _service;

  public UlmDslClient(HttpClient httpClient)
  {
    _service = new UlmDslService(httpClient);
  }

  public IReadOnlyList<UlmDslMail> GetMails(string name) => GetMailsAsync(name).Result;

  public async Task<IReadOnlyList<UlmDslMail>> GetMailsAsync(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Invalid name");

    var mails = new List<UlmDslMail>();

    var mailInfos = await GetInboxAsync(name);

    foreach (var mailInfo in mailInfos)
    {
      var feed = await _service.FetchMailFeedAsync(name, mailInfo.Id);

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

  public UlmDslMail? GetMailById(string name, int id) => GetMailByIdAsync(name, id).Result;

  public async Task<UlmDslMail?> GetMailByIdAsync(string name, int id)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Invalid name");

    var mailInfos = await GetInboxAsync(name);

    var mailInfo = mailInfos.SingleOrDefault(mail => mail.Id == id);

    if (mailInfo is null)
      return null;

    var feed = await _service.FetchMailFeedAsync(name, id);

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

  public IReadOnlyList<UlmDslMailInfo> GetInbox(string name) => GetInboxAsync(name).Result;

  public async Task<IReadOnlyList<UlmDslMailInfo>> GetInboxAsync(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Invalid name");

    var feed = await _service.FetchInboxFeedAsync(name);

    return feed.Items
      .Where(item => !string.IsNullOrWhiteSpace(item.Summary.Text))
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
