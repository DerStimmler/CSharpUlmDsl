namespace UlmDslClient;

internal static class ApiAdresses
{
  internal static readonly Uri BaseAddress = new("https://ulm-dsl.de");
  public static string InboxApi(string name) => $"/inbox-api.php?name={name}";
  public static string MailApi(string name, int id) => $"/mail-api.php?name={name}&id={id}";
}
