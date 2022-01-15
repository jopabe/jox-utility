namespace Jox.Utility;

public class InternetShortcut
{
    public string TargetUrl { get; private set; }

    public string LocalPath { get; private set; }

    public InternetShortcut(string pathAndName, string url)
    {
        LocalPath = pathAndName + ".url";
        TargetUrl = url;
    }

    public void Create()
    {
        using var output = File.CreateText(LocalPath);
        output.WriteLine("[InternetShortcut]");
        output.WriteLine("URL={0}", TargetUrl);
    }
}
