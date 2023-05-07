# CSharpUlmDsl

[![dotnet](https://img.shields.io/badge/platform-.NET-blue)](https://www.nuget.org/packages/CSharpUlmDsl/)
[![nuget version](https://img.shields.io/nuget/v/CSharpUlmDsl)](https://www.nuget.org/packages/CSharpUlmDsl/)
[![nuget downloads](https://img.shields.io/nuget/dt/CSharpUlmDsl)](https://www.nuget.org/packages/CSharpUlmDsl/)
![build](https://github.com/DerStimmler/CSharpUlmDsl/actions/workflows/build.yml/badge.svg)
[![codecov](https://codecov.io/gh/DerStimmler/CSharpUlmDsl/branch/master/graph/badge.svg?token=HL0P0ND9ZF)](https://codecov.io/gh/DerStimmler/CSharpUlmDsl)
[![GitHub license](https://img.shields.io/github/license/DerStimmler/CSharpUlmDsl)](https://github.com/DerStimmler/CSharpUlmDsl/blob/master/LICENSE.md)

C# client for fetching emails from the temp mail service [ulm-dsl](https://ulm-dsl.de/).

## Installation

Available on [NuGet](https://www.nuget.org/packages/CSharpUlmDsl/).

```bash
dotnet add package CSharpUlmDsl
```

or

```powershell
PM> Install-Package CSharpUlmDsl
```

## Usage

The following examples use the email address `max.mustermann@ulm-dsl.de`. Just replace the inbox name `max.mustermann`
to match your address.

Note that you have to fetch your inbox once before you can receive emails at your address. Your address stays active for 14 days. This period renews for every request.

### Initialization

First create an instance of `UlmDslClient` by passing an instance of `HttpClient` to its constructor.

```csharp
var httpClient = new HttpClient();

var client = new UlmDslClient(httpClient);
```

### Get Inbox

You can fetch the basic information **except the body** for all emails in an inbox by calling the `GetInbox`
/ `GetInboxAsync` methods and passing the inbox name.

```csharp
var emails = client.GetInbox("max.mustermann");
```

```csharp
var emails = await client.GetInboxAsync("max.mustermann");
```

### Get Mail by Id

You can get all available information for a specific email by calling the `GetMailById` / `GetMailByIdAsync` methods and
passing the inbox name and email identifier.

```csharp
var email = client.GetMailById("max.mustermann", 7);
```

```csharp
var email = await client.GetMailByIdAsync("max.mustermann", 7);
```

### Get Mails

You can get all available information for all emails in an inbox by calling the `GetMails` / `GetMailsAsync` methods and
passing the inbox name.

```csharp
var emails = client.GetMails("max.mustermann");
```

```csharp
var emails = await client.GetMailsAsync("max.mustermann");
```

### Dependency Injection

You can register the `UlmDslClient` in your Startup with a typed `HttpClient`.

```csharp
builder.Services.AddHttpClient<UlmDslClient>();
```

Then inject the client wherever you like. E.g. in a controller:

```csharp
[Route("Home")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly UlmDslClient _client;

    public HomeController(UlmDslClient client)
    {
        _client = client;
    }
}
```

### API rate limits

The api is limited to about 100 requests per minute. So you should keep in mind when this client fires requests:

- When you fetch your inbox only a single request gets fired.
- When you fetch a specific email by id, two requests get fired. One for the basic information e.g. the sender and
  receiver and one for the email body.
- When you fetch all emails a first request gets fired for the basic information of all emails and then another request
  per email to retrieve each email body.

So based on your inbox size you should think twice before you fetch all emails. Maybe it's better to just fetch the
inbox and then retrieve a single email by id.

## Related

Here are some related projects:

- [ts-ulm-dsl](https://github.com/DerStimmler/ts-ulm-dsl): Typescript version of this library
