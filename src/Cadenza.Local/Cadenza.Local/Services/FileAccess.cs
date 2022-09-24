using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Model;

namespace Cadenza.Local.Services;

public class FileAccess : IFileAccess
{
    public Task<List<LocalFile>> GetFiles(string directoryPath, List<string> extensions)
    {
        var files = new List<LocalFile>();

        if (!Directory.Exists(directoryPath))
            return Task.FromResult(files);

        foreach (var filepath in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
        {
            var fileInfo = new FileInfo(filepath);
            if (extensions.Contains(fileInfo.Extension))
            {
                files.Add(new LocalFile
                {
                    Path = fileInfo.FullName,
                    DateCreated = fileInfo.CreationTime,
                    DateModified = fileInfo.LastWriteTime
                });
            }
        }

        return Task.FromResult(files);
    }

    public Task DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        return Task.CompletedTask;
    }
}