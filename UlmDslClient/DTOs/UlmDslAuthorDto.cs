using System.Xml.Serialization;

namespace UlmDslClient.DTOs;

public struct UlmDslAuthorDto
{
  [XmlElement(ElementName = "name")]
  public string Name { get; set; }
}