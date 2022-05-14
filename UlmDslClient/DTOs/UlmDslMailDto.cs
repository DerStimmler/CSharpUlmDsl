using System.Xml.Serialization;

namespace UlmDslClient.DTOs;

[XmlRoot("feed", Namespace ="http://www.w3.org/2005/Atom" )]
public struct UlmDslMailDto
{
  [XmlElement(ElementName = "author")]
  public UlmDslAuthorDto Author { get; set; }
  [XmlElement(ElementName = "title")]
  public string Title { get; set; }
  [XmlElement(ElementName = "id")]
  public string Id { get; set; }
  [XmlElement(ElementName = "updated")]
  public string Updated { get; set; }
  [XmlElement(ElementName = "entry")]
  public UlmDslMailEntryDto Entry { get; set; }
}
