namespace Jox.Utility;

public static class CsvTextWriterExtensions
{
    public static void WriteRawCsvLine(this TextWriter output, char separator, params string[] values)
    {
#if NET
            ArgumentNullException.ThrowIfNull(output);
#endif

        output.WriteLine(string.Join(separator.ToString(), values));
    }

    public static void WriteQuotedCsvLine(this TextWriter output, char separator, params string[] values)
    {
#if NET
            ArgumentNullException.ThrowIfNull(output);
#endif

        var builder = new StringBuilder();
        foreach (var value in values)
        {
            if (builder.Length > 0) builder.Append(separator);

            builder.Append('"');
            foreach (var chr in value)
            {
                // escape existing double quotes
                if (chr == '"') builder.Append('"');

                builder.Append(chr);
            }
            builder.Append('"');
        }
        output.WriteLine(builder.ToString());
    }
}
