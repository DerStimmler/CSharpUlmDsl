using System.Xml.Serialization;

namespace UlmDslClient.DTOs;

public struct UlmDslMailEntryDto
{
  [XmlElement(ElementName = "title")]
  public string Title { get; set; }
  [XmlElement(ElementName = "link")]
  public UlmDslLinkDto Link { get; set; }
  [XmlElement(ElementName = "id")]
  public int Id { get; set; }
  [XmlElement(ElementName = "updated")]
  public string Updated { get; set; }
  [XmlElement(ElementName = "summary")]
  public string Summary { get; set; }
  [XmlElement(ElementName = "content")]
  public string Content { get; set; }
}
