namespace UlmDslClient.Models;

public record UlmDslMail : UlmDslMailInfo
{
  public string Body { get; set; }
}
