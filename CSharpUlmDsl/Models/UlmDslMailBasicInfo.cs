namespace CSharpUlmDsl.Models;

public record UlmDslMailBasicInfo
{
  /// <summary>
  ///   Email identifier.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  ///   Subject of the email.
  /// </summary>
  public string Subject { get; set; }

  /// <summary>
  ///   Uri to open mail in browser.
  /// </summary>
  public Uri Link { get; set; }

  /// <summary>
  ///   Information about the recipient of the email.
  /// </summary>
  public UlmDslMailRecipient Recipient { get; set; }

  /// <summary>
  ///   Information about the sender of the email.
  /// </summary>
  public UlmDslMailSender Sender { get; set; }

  /// <summary>
  ///   Receiving time.
  /// </summary>
  public DateTimeOffset Date { get; set; }
}
