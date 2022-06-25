using System.Net.Mail;

namespace UlmDslClient.Utils;

internal static class StringUtils
{
  internal static string DecodeQuotedPrintable(string text)
  {
    var attachment = Attachment.CreateAttachmentFromString("", text.Trim());

    return attachment.Name.Trim();
  }
}
