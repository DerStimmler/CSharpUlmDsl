namespace UlmDslClient;

internal static class ApiAdresses
{
  private static readonly Uri BaseAddress = new("https://ulm-dsl.de");
  internal static Uri InboxApi(string name) => new($"{BaseAddress}/inbox-api.php?name={name}");
  internal static Uri MailApi(string name, int id) => new($"{BaseAddress}/mail-api.php?name={name}&id={id}");
}
