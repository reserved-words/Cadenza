using Cadenza.API.Database.Interfaces;

namespace Cadenza.API.Database.Services;

internal class FileAccess : IFileAccess
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
}