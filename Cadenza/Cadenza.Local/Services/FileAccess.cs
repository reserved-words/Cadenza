namespace Cadenza.Local;

public class FileAccess : IFileAccess
{
    public string GetText(string path)
    {
        return File.Exists(path)
            ? File.ReadAllText(path)
            : null;
    }

    public void SaveText(string path, string text)
    {
        var directory = Path.GetDirectoryName(path);
        Directory.CreateDirectory(directory);
        File.WriteAllText(path, text);
    }

    public List<LocalFile> GetFiles(string directoryPath, List<string> extensions)
    {
        if (!Directory.Exists(directoryPath))
            return new List<LocalFile>();

        var files = new List<LocalFile>();

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

        return files;
    }

    public void DeleteFile(string path)
    {
        if (!File.Exists(path))
            return;

        File.Delete(path);
    }
}