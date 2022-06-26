using System.Net.Mail;

namespace CSharpUlmDsl.Utils;

internal static class StringUtils
{
  internal static string DecodeQuotedPrintable(string text)
  {
    var attachment = Attachment.CreateAttachmentFromString("", text.Trim());

    return attachment.Name.Trim();
  }
}
