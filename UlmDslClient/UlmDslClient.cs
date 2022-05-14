namespace UlmDslClient;

public class UlmDslClient
{
  private readonly UlmDslService _service;

  public UlmDslClient(HttpClient httpClient)
  {
    _service = new UlmDslService(httpClient);
  }

  public async Task<IReadOnlyList<UlmDslMail>> GetMailsAsync(string name)
  {
    var mails = new List<UlmDslMail>();

    var mailInfos = await GetMailInfosAsync(name);

    foreach (var mailInfo in mailInfos)
    {
      var mail = await _service.FetchMailAsync(name, mailInfo.Id);

      mails.Add(new UlmDslMail
      {
        Id = mail.Entry.Id,
        Link = new Uri(mail.Entry.Link.Href),
        Subject = mail.Entry.Title,
        Updated = DateTimeOffset.Parse(mail.Entry.Updated),
        Date = mailInfo.Date,
        Recipient = mailInfo.Recipient,
        Sender = mailInfo.Sender,
        Body = ExtractBodyFromContent(mail.Entry.Content)
      });
    }

    return mails;
  }

  private string ExtractBodyFromContent(string content) =>
    content.Replace("<![CDATA[ ", string.Empty).Replace("]]>", string.Empty).Trim();

  public async Task<UlmDslMail> GetMailByIdAsync(string name, int id)
  {
    var mailInfos = await GetMailInfosAsync(name);
    var mailInfo = mailInfos.SingleOrDefault(mail => mail.Id == id);

    if (mailInfo is null)
      throw new ArgumentException($"There is no mail with id {id}");

    var mail = await _service.FetchMailAsync(name, id);

    return new UlmDslMail
    {
      Id = mail.Entry.Id,
      Link = new Uri(mail.Entry.Link.Href),
      Subject = mail.Entry.Title,
      Updated = DateTimeOffset.Parse(mail.Entry.Updated),
      Date = mailInfo.Date,
      Recipient = mailInfo.Recipient,
      Sender = mailInfo.Sender,
      Body = ExtractBodyFromContent(mail.Entry.Content)
    };
  }

  public async Task<IReadOnlyList<UlmDslMailInfo>> GetMailInfosAsync(string name)
  {
    var inbox = await _service.FetchInboxAsync(name);

    return inbox.Entries.Select(entry => new UlmDslMailInfo
    {
      Id = entry.Id,
      Link = new Uri(entry.Link.Href),
      Subject = entry.Title,
      Updated = DateTimeOffset.Parse(entry.Updated),
      Date = ExtractDateFromSummary(entry.Summary),
      Recipient = ExtractRecipientFromSummary(entry.Summary),
      Sender = ExtractSenderFromSummary(entry.Summary)
    }).ToList();
  }

  private UlmDslMailRecipient ExtractRecipientFromSummary(string summary)
  {
    var recipient = summary.Split("\n")[2].Split("=>").Last();

    var recipientDisplayName = recipient.Split("<").First().Trim();
    var recipientEmail = recipient.Split("<").Last().Replace(">", string.Empty).Trim();

    return new UlmDslMailRecipient
    {
      DisplayName = recipientDisplayName,
      Email = recipientEmail
    };
  }

  private UlmDslMailSender ExtractSenderFromSummary(string summary)
  {
    var sender = summary.Split("\n")[1].Split("=>").Last();

    var senderDisplayName = sender.Split("<").First().Trim();
    var senderEmail = sender.Split("<").Last().Replace(">", string.Empty).Trim();

    return new UlmDslMailSender
    {
      DisplayName = senderDisplayName,
      Email = senderEmail
    };
  }

  private DateTimeOffset ExtractDateFromSummary(string summary) =>
    DateTimeOffset.Parse(summary.Split("\n")[3].Split("=>").Last());
}
