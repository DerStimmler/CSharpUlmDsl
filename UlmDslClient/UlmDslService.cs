using System.Xml.Serialization;
using UlmDslClient.DTOs;

namespace UlmDslClient;

internal class UlmDslService
{
  private readonly HttpClient _httpClient;

  public UlmDslService(HttpClient httpClient)
  {
    httpClient.BaseAddress = ApiAdresses.BaseAddress;

    _httpClient = httpClient;
  }

  internal async Task<UlmDslMailDto> FetchMailAsync(string name, int id)
  {
    var response = await _httpClient.GetAsync(ApiAdresses.MailApi(name, id));

    var content = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
      throw new InvalidOperationException(content);

    var stringReader = new StringReader(content);

    var xmlSerializer = new XmlSerializer(typeof(UlmDslMailDto));

    var dto = xmlSerializer.Deserialize(stringReader);

    return (UlmDslMailDto) dto;
  }

  internal async Task<UlmDslInboxDto> FetchInboxAsync(string name)
  {
    var response = await _httpClient.GetAsync(ApiAdresses.InboxApi(name));

    var content = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
      throw new InvalidOperationException(content);

    var stringReader = new StringReader(content);

    var xmlSerializer = new XmlSerializer(typeof(UlmDslInboxDto));

    var dto = xmlSerializer.Deserialize(stringReader);

    return (UlmDslInboxDto) dto;
  }
}
