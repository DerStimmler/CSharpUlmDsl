using System.Xml.Serialization;

namespace UlmDslClient.DTOs;

public struct UlmDslLinkDto
{
  [XmlAttribute("href")]
  public string Href { get; set; }
}
