namespace CSharpUlmDsl.Models;

/// <summary>
///   Contains information about the sender
/// </summary>
/// <param name="DisplayName"></param>
/// <param name="Email"></param>
public record struct UlmDslMailSender(string DisplayName, string Email);
