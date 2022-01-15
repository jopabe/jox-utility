namespace Jox.Utility.Filter;

public interface ILineFilter
{
    void Initialize();
    void Process(string line);
    void Flush();
}
