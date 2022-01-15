namespace Jox.Utility;

public class CsvOutput : IDisposable
{
    private TextWriter output;

    public char Separator { get; set; } = ',';
    public bool AddQuotes { get; set; } = true;

    public CsvOutput(TextWriter outputStream)
    {
        output = outputStream;
    }
    
    public CsvOutput(string filename)
    {
        output = new FileInfo(filename).CreateText();
    }

    public void WriteCsvLine(params string[] values)
    {
        output.WriteLine(string.Join(Separator.ToString(), values.Select(x => AddQuotes ? '"' + x + '"' : x)));
    }

    public void Flush() => output.Flush();

    public void Dispose()
    {
        output?.Dispose();
        output = null;
    }
}
