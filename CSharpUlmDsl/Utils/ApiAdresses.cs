namespace CSharpUlmDsl.Utils;

internal static class ApiAdresses
{
  internal static readonly Uri BaseAddress = new("https://ulm-dsl.de/");
  internal static string InboxApi(string name) => $"inbox-api.php?name={name}";
  internal static string MailApi(string name, int id) => $"mail-api.php?name={name}&id={id}";
}
