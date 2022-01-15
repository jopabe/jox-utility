namespace Jox.Utility.Filter;

public abstract class ChainedFilter : ILineFilter
{
    protected internal ILineFilter Next { get; internal set; }

    protected ChainedFilter() { }

    public abstract void Process(string line);
    public virtual void InitializeFilter() { }
    public void Initialize()
    {
        InitializeFilter();
        Next.Initialize();
    }
    public virtual void FlushFilter() { }
    public void Flush()
    {
        FlushFilter();
        Next.Flush();
    }

    protected void Pass(string line) => Next?.Process(line);
}
