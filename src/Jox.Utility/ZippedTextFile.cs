using System.IO.Compression;

namespace Jox.Utility;

public class ZippedTextFile : IDisposable
{
    private FileStream zipStream;
    private ZipArchive zipArchive;
    private TextWriter stream;
    public FileInfo ZipFile { get; }

    public ZippedTextFile(FileInfo zipFile) => ZipFile = zipFile;
    public ZippedTextFile(string path) : this(new FileInfo(path)) { }

    public TextWriter OpenStream()
    {
        if (stream == null)
        {
            zipStream = ZipFile.Create();
            try
            {
                zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create);
                ZipArchiveEntry textFile = zipArchive.CreateEntry(Path.GetFileNameWithoutExtension(ZipFile.Name), CompressionLevel.Optimal);
                stream = new StreamWriter(textFile.Open());
            }
            catch (Exception)
            {
                zipArchive?.Dispose();
                zipStream?.Dispose();
                throw;
            }
        }
        return stream;
    }

    public virtual void Dispose()
    {
        stream?.Dispose();
        zipArchive?.Dispose();
        zipStream?.Dispose();
    }
}
