using System.Xml;

namespace Jox.Utility;

public static class AsciiXmlTextWriterExtensions
{
    public static void WriteNonAsciiAsEntities(this XmlTextWriter output, string text)
    {
        var sb = new StringBuilder(text.Length);
        foreach (char c in text)
        {
            int codepoint = Convert.ToInt32(c);
            if (codepoint < 0 || codepoint > 127)
            {
                sb.Append("&#");
                sb.Append(codepoint);
                sb.Append(';');
            }
            else
            {
                sb.Append(c);
            }
        }
        output.WriteRaw(sb.ToString());
    }
}
