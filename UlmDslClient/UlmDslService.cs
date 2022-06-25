using System.ServiceModel.Syndication;
using System.Xml.Linq;

namespace UlmDslClient;

internal static class UlmDslService
{
  internal static SyndicationFeed FetchMailFeed(string name, int id)
  {
    var uri = ApiAdresses.MailApi(name, id).AbsoluteUri;

    var document = XDocument.Load(uri);

    return SyndicationFeed.Load(document.CreateReader());
  }

  internal static SyndicationFeed FetchInboxFeed(string name)
  {
    var uri = ApiAdresses.InboxApi(name).AbsoluteUri;

    var document = XDocument.Load(uri);

    return SyndicationFeed.Load(document.CreateReader());
  }
}
