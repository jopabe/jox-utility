using System.Runtime.InteropServices;

namespace Jox.Utility;

public class KillSignalCancellationTokenSource : IDisposable
{
    private CancellationTokenSource tokenSource;
    public CancellationToken Token => tokenSource.Token;
    private HandlerRoutine handlerRoutine;
    private Mutex mutex;

    [DllImport("Kernel32")]
    public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);
    public delegate bool HandlerRoutine(CtrlType ctrlType);

    public enum CtrlType
    {
        CTRL_C_EVENT = 0,
        CTRL_BREAK_EVENT,
        CTRL_CLOSE_EVENT,
        CTRL_LOGOFF_EVENT = 5,
        CTRL_SHUTDOWN_EVENT
    }

    public KillSignalCancellationTokenSource()
    {
        mutex = new Mutex(true);
        tokenSource = new CancellationTokenSource();
        handlerRoutine = Cancel;
        SetConsoleCtrlHandler(handlerRoutine, true);
    }

    private bool Cancel(CtrlType ctrlType)
    {
        tokenSource.Cancel();
        mutex.WaitOne();
        return true;
    }

    public void Dispose() => mutex.ReleaseMutex();
}
