using Cadenza.Domain.Models;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.Utilities.Implementations;

internal class FileAccess : IFileAccess
{
    public Task DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        return Task.CompletedTask;
    }

    public Task<List<FileDetails>> GetFiles(string directoryPath, List<string> extensions = null)
    {
        var files = new List<FileDetails>();

        if (!Directory.Exists(directoryPath))
            return Task.FromResult(files);

        foreach (var filepath in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
        {
            var fileInfo = new FileInfo(filepath);
            if (extensions == null || extensions.Contains(fileInfo.Extension))
            {
                files.Add(new FileDetails
                {
                    Path = fileInfo.FullName,
                    DateCreated = fileInfo.CreationTime,
                    DateModified = fileInfo.LastWriteTime
                });
            }
        }

        return Task.FromResult(files);
    }

    public async Task<string> GetText(string path)
    {
        return File.Exists(path)
            ? await File.ReadAllTextAsync(path)
            : null;
    }

    public async Task SaveText(string path, string text)
    {
        var directory = Path.GetDirectoryName(path);
        Directory.CreateDirectory(directory);
        await File.WriteAllTextAsync(path, text);
    }
}
