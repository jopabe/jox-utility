namespace Jox.Utility.Filter;

public class FileWriterSink : LineSink, IDisposable
{
    public FileWriterSink(string path)
    {
        Output = File.CreateText(path);
    }

    public FileWriterSink(string path, Encoding encoding)
    {
        Output = new StreamWriter(path, false, encoding);
    }

    public void Dispose() => Output?.Dispose();
}
