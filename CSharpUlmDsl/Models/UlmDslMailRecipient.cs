namespace CSharpUlmDsl.Models;

/// <summary>
///   Contains information about the recipient
/// </summary>
/// <param name="DisplayName"></param>
/// <param name="Email"></param>
public record struct UlmDslMailRecipient(string DisplayName, string Email);
