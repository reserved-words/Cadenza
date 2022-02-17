namespace Cadenza.Local;

public class FileAccess : IFileAccess
{
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