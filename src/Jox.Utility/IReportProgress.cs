namespace Jox.Utility;

public interface IReportProgress
{
    void Tick(string message = null);
    void UpdateMaxTicks(int maxTicks);
    void UpdateMessage(string message);
}
