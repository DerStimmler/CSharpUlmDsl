using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using CSharpUlmDsl.Utils;

namespace CSharpUlmDsl;

internal class UlmDslService
{
  private readonly HttpClient _httpClient;

  internal UlmDslService(HttpClient httpClient)
  {
    httpClient.BaseAddress = ApiAdresses.BaseAddress;

    _httpClient = httpClient;
  }

  internal async Task<SyndicationFeed> FetchMailFeedAsync(string name, int id)
  {
    var uri = ApiAdresses.MailApi(name, id);

    return await FetchFeedAsync(uri);
  }

  internal async Task<SyndicationFeed> FetchInboxFeedAsync(string name)
  {
    var uri = ApiAdresses.InboxApi(name);

    return await FetchFeedAsync(uri);
  }

  private async Task<SyndicationFeed> FetchFeedAsync(string uri)
  {
    var response = await _httpClient.GetAsync(uri);

    var content = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
      throw new InvalidOperationException(content);

    var document = XDocument.Parse(content);

    ClearInvalidElements(document);

    return SyndicationFeed.Load(document.CreateReader());
  }

  private static void ClearInvalidElements(XContainer document)
  {
    var invalidElements = document
      .Descendants()
      .Where(element => element.Name.LocalName is "content" or "summary")
      .Where(element => element.LastNode.NodeType != XmlNodeType.CDATA)
      .ToList();

    foreach (var element in invalidElements) element.Value = string.Empty;
  }
}
