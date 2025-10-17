using CellSync.Domain.Entities;
using CellSync.Domain.Files;

namespace CellSync.Api.Adapters;

public class FormFileAdapter(IFormFile file) : IUploadedFile
{
    public string FileName => file.FileName;
    public string ContentType => file.ContentType;
    public long FileSize => file.Length;
    public Stream GetFileContentStream() => file.OpenReadStream();
}