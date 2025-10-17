namespace CellSync.Domain.Files;

public interface IUploadedFile
{
    string FileName { get; }
    string ContentType { get; }
    long FileSize { get; }
    Stream GetFileContentStream();
}