namespace UlmDslClient;

public class UlmDslMailInfo
{
  public int Id { get; set; }
  public string Subject { get; set; }
  public Uri Link { get; set; }
  public DateTimeOffset Updated { get; set; }
  public UlmDslMailRecipient Recipient { get; set; }
  public UlmDslMailSender Sender { get; set; }
  public DateTimeOffset Date { get; set; }
}
