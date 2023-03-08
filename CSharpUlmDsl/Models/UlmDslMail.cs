namespace CSharpUlmDsl.Models;

/// <summary>
///   Provides full information about an ulm-dsl mail including the body/content
/// </summary>
public record UlmDslMail : UlmDslMailBasicInfo
{
  /// <summary>
  ///   Content of the email.
  /// </summary>
  public string Body { get; set; } = default!;
}
