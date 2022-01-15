    namespace Jox.Utility.Filter;

public class FilterChain
{
    public Encoding Encoding { get; set; } = new UTF8Encoding(false);
    private ChainedFilter First { get; set; }
    private ChainedFilter Last { get; set; }

    public FilterChain() { }
    public FilterChain(Encoding encoding) { Encoding = encoding; }

    public FilterChain Add(ChainedFilter filter)
    {
        if (Last == null)
        {
            Last = filter;
        }
        else
        {
            filter.Next = First;
        }
        First = filter;
        return this;
    }

    public FilterChain Add<T>() where T : ChainedFilter, new() => Add(new T());

    public void EditInPlace(string path)
    {
        const string extension = ".bak";
        var orig = path + extension;
        for (int i = 1; File.Exists(orig); i++)
        {
            orig = string.Concat(path, ".", i, extension);
        }
        File.Copy(path, orig);

        using (var sink = new FileWriterSink(path, Encoding))
        {
            Last.Next = sink;
            First.Initialize();
            foreach (var line in File.ReadAllLines(orig, Encoding))
            {
                First.Process(line);
            }
            First.Flush();
        }

        File.Delete(orig);
    }
}
