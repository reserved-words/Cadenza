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
        File.WriteAllText(path, text);
    }

    public IEnumerable<LocalFile> GetFiles(string directoryPath, List<string> extensions)
    {
        foreach (var filepath in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
        {
            var fileInfo = new FileInfo(filepath);
            if (extensions.Contains(fileInfo.Extension))
            {
                yield return new LocalFile
                {
                    Path = fileInfo.FullName,
                    DateCreated = fileInfo.CreationTime,
                    DateModified = fileInfo.LastWriteTime
                };
            }
        }
    }

    public void DeleteFile(string path)
    {
        File.Delete(path);
    }
}