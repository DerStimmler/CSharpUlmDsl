using System;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Xunit;

namespace CSharpUlmDsl.Tests;

public class UlmDslClientTest
{
  private static HttpClient GetMockedHttpClient()
  {
    var mockHttp = new MockHttpMessageHandler();

    mockHttp.When("https://ulm-dsl.de/inbox-api.php?name=max.mustermann").Respond("text/xml", ResponseMocks.InboxXml);
    mockHttp.When("https://ulm-dsl.de/inbox-api.php?name=stimmler").Respond("text/xml", ResponseMocks.EmptyInboxXml);
    mockHttp.When("https://ulm-dsl.de/mail-api.php?name=max.mustermann&id=5267")
      .Respond("text/xml", ResponseMocks.SingleMail5267Xml);
    mockHttp.When("https://ulm-dsl.de/mail-api.php?name=max.mustermann&id=4305")
      .Respond("text/xml", ResponseMocks.SingleMail4305Xml);
    mockHttp.When("https://ulm-dsl.de/mail-api.php?name=max.mustermann&id=4")
      .Respond("text/xml", ResponseMocks.InvalidId4Xml);
    mockHttp.When("https://ulm-dsl.de/mail-api.php?name=server-error")
      .Respond(_ => new HttpResponseMessage(HttpStatusCode.InternalServerError));

    return new HttpClient(mockHttp);
  }

  [Fact]
  public async void GetInboxAsync()
  {
    var client = new UlmDslClient(GetMockedHttpClient());
    var inbox = await client.GetInboxAsync("max.mustermann");

    inbox.Should().HaveCount(2);

    inbox.Should().BeEquivalentTo(ResponseMocks.Inbox);
  }

  [Fact]
  public void GetInbox()
  {
    var client = new UlmDslClient(GetMockedHttpClient());
    var inbox = client.GetInbox("max.mustermann");

    inbox.Should().HaveCount(2);

    inbox.Should().BeEquivalentTo(ResponseMocks.Inbox);
  }

  [Fact]
  public async void EmptyInbox()
  {
    var client = new UlmDslClient(GetMockedHttpClient());
    var inbox = await client.GetInboxAsync("stimmler");

    inbox.Should().HaveCount(0);
  }

  [Fact]
  public async void GetMailByIdAsync()
  {
    var client = new UlmDslClient(GetMockedHttpClient());
    var mail = await client.GetMailByIdAsync("max.mustermann", 5267);

    mail.Should().Be(ResponseMocks.SingleMail5267);
  }

  [Fact]
  public void GetMailById()
  {
    var client = new UlmDslClient(GetMockedHttpClient());
    var mail = client.GetMailById("max.mustermann", 5267);

    mail.Should().Be(ResponseMocks.SingleMail5267);
  }

  [Fact]
  public async void GetMailsAsync()
  {
    var client = new UlmDslClient(GetMockedHttpClient());
    var mails = await client.GetMailsAsync("max.mustermann");

    mails.Count.Should().Be(2);
    mails.Should().Contain(ResponseMocks.SingleMail5267);
    mails.Should().Contain(ResponseMocks.SingleMail4305);
  }

  [Fact]
  public void GetMails()
  {
    var client = new UlmDslClient(GetMockedHttpClient());
    var mails = client.GetMails("max.mustermann");

    mails.Count.Should().Be(2);
    mails.Should().Contain(ResponseMocks.SingleMail5267);
    mails.Should().Contain(ResponseMocks.SingleMail4305);
  }

  [Fact]
  public async void InvalidId()
  {
    var client = new UlmDslClient(GetMockedHttpClient());
    var result = await client.GetMailByIdAsync("max.mustermann", 4);

    result.Should().BeNull();
  }

  [Fact]
  public async void InvalidName()
  {
    var client = new UlmDslClient(GetMockedHttpClient());

    var result = async () => { await client.GetMailByIdAsync(string.Empty, 4); };
    await result.Should().ThrowAsync<ArgumentException>();
    var result2 = async () => { await client.GetInboxAsync(string.Empty); };
    await result2.Should().ThrowAsync<ArgumentException>();
    var result3 = async () => { await client.GetMailsAsync(string.Empty); };
    await result3.Should().ThrowAsync<ArgumentException>();
  }

  [Fact]
  public async void ServerError()
  {
    var client = new UlmDslClient(GetMockedHttpClient());

    var result = async () => { await client.GetInboxAsync("server-error"); };
    await result.Should().ThrowAsync<InvalidOperationException>();
  }
}
