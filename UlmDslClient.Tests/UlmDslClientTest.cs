using System.Net.Http;
using Xunit;

namespace UlmDslClient.Tests;

public class UlmDslClientTest
{
  [Fact]
  public async void GetMailInfos()
  {
    var httpClient = new HttpClient();

    var ulmDslClient = new UlmDslClient(httpClient);

    var x = await ulmDslClient.GetMailInfosAsync("stimmler123");
  }

  [Fact]
  public async void GetMailById()
  {
    var httpClient = new HttpClient();

    var ulmDslClient = new UlmDslClient(httpClient);

    var x = await ulmDslClient.GetMailByIdAsync("stimmler123", 7315);
  }

  [Fact]
  public async void GetMails()
  {
    var httpClient = new HttpClient();

    var ulmDslClient = new UlmDslClient(httpClient);

    var x = await ulmDslClient.GetMailsAsync("stimmler123");
  }
}
