namespace Jox.Utility.Filter;

public class LineSink : ILineFilter
{
    protected TextWriter Output { get; set; }
    public void Process(string line) => Output?.WriteLine(line);
    public void WriteWithoutNewline(string line) => Output?.Write(line);

    public void Initialize() { }

    public void Flush() => Output.Flush();

    public static LineSink ConsoleOutSink => new LineSink() { Output = Console.Out };
}
