namespace CSharpUlmDsl.Models;

public record UlmDslMail : UlmDslMailBasicInfo
{
  /// <summary>
  ///   Content of the email.
  /// </summary>
  public string Body { get; set; }
}
