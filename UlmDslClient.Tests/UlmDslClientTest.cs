using Xunit;

namespace UlmDslClient.Tests;

public class UlmDslClientTest
{
  [Fact]
  public void GetMailInfos()
  {
    var x = UlmDslClient.GetMailInfos("max.mustermann");
  }

  [Fact]
  public void GetMailById()
  {
    var x = UlmDslClient.GetMailById("max.mustermann", 7315);
  }

  [Fact]
  public void GetMails()
  {
    var x = UlmDslClient.GetMails("max.mustermann");
  }
}
