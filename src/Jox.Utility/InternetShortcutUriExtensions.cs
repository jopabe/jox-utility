namespace Jox.Utility;

public static class InternetShortcutUriExtensions
{
    public static void WriteInternetShortcut(this Uri uri, TextWriter output)
    {
#if NET
        ArgumentNullException.ThrowIfNull(uri);
        ArgumentNullException.ThrowIfNull(output);
#endif

        output.WriteLine("[InternetShortcut]");
        output.Write("URL=");
        output.WriteLine(uri.AbsoluteUri);
    }

    public static void WriteInternetShortcut(this Uri uri, FileInfo file)
    {
#if NET
        ArgumentNullException.ThrowIfNull(uri);
        ArgumentNullException.ThrowIfNull(file);
#endif

        if (!file.Extension.Equals(".url", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The output file name should use the .url extension", nameof(file));
        }

        using var output = file.CreateText();
        uri.WriteInternetShortcut(output);
    }

    public static void WriteInternetShortcut(this Uri uri, string filePath)
    {
#if NET
        ArgumentNullException.ThrowIfNull(uri);
        ArgumentNullException.ThrowIfNull(filePath);
#endif

        if (!filePath.EndsWith(".url", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The output file name should use the .url extension", nameof(filePath));
        }

        using var output = File.CreateText(filePath);
        uri.WriteInternetShortcut(output);
    }
}
