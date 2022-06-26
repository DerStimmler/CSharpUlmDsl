using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using CSharpUlmDsl.Models;
using CSharpUlmDsl.Utils;

namespace CSharpUlmDsl;

public class UlmDslClient
{
  private readonly UlmDslService _service;

  public UlmDslClient(HttpClient httpClient)
  {
    _service = new UlmDslService(httpClient);
  }

  /// <summary>
  ///   Get all mails for a given inbox name.
  /// </summary>
  /// <param name="name">inbox name (everything before @ulm-dsl.de)</param>
  /// <returns>List of emails with all available information.</returns>
  /// <exception cref="ArgumentException">In case the inbox name is invalid.</exception>
  public IReadOnlyList<UlmDslMail> GetMails(string name) => GetMailsAsync(name).Result;

  /// <summary>
  ///   Get all emails for a given inbox name asynchronously.
  /// </summary>
  /// <param name="name">inbox name (everything before @ulm-dsl.de)</param>
  /// <returns>List of emails with all available information.</returns>
  /// <exception cref="ArgumentException">In case the inbox name is invalid.</exception>
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

  /// <summary>
  ///   Gets a specific email based on inbox name and email identifier.
  /// </summary>
  /// <param name="name">inbox name (everything before @ulm-dsl.de)</param>
  /// <param name="id">identifier of email</param>
  /// <returns>Specific email with all available information.</returns>
  /// <exception cref="ArgumentException">In case the inbox name is invalid.</exception>
  public UlmDslMail? GetMailById(string name, int id) => GetMailByIdAsync(name, id).Result;

  /// <summary>
  ///   Gets a specific email based on inbox name and email identifier asynchronously.
  /// </summary>
  /// <param name="name">inbox name (everything before @ulm-dsl.de)</param>
  /// <param name="id">identifier of email</param>
  /// <returns>Specific email with all available information.</returns>
  /// <exception cref="ArgumentException">In case the inbox name is invalid.</exception>
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

  /// <summary>
  ///   Get basic info of all emails for a given inbox name.
  /// </summary>
  /// <param name="name">inbox name (everything before @ulm-dsl.de)</param>
  /// <returns>List of emails with basic information except body.</returns>
  /// <exception cref="ArgumentException">In case the inbox name is invalid.</exception>
  public IReadOnlyList<UlmDslMailBasicInfo> GetInbox(string name) => GetInboxAsync(name).Result;

  /// <summary>
  ///   Get basic info of all emails for a given inbox name asynchronously.
  /// </summary>
  /// <param name="name">inbox name (everything before @ulm-dsl.de)</param>
  /// <returns>List of emails with basic information except body.</returns>
  /// <exception cref="ArgumentException">In case the inbox name is invalid.</exception>
  public async Task<IReadOnlyList<UlmDslMailBasicInfo>> GetInboxAsync(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Invalid name");

    var feed = await _service.FetchInboxFeedAsync(name);

    return feed.Items
      .Where(item => !string.IsNullOrWhiteSpace(item.Summary.Text))
      .Select(item => new UlmDslMailBasicInfo
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
