﻿namespace Cadenza.Local.FileAccess;

internal class Service : IFileAccess
{
    public void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public List<FileDetails> GetFiles(string directoryPath, List<string> extensions = null)
    {
        var files = new List<FileDetails>();

        if (!Directory.Exists(directoryPath))
            return files;

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

        return files;
    }

    public string GetText(string path)
    {
        return File.Exists(path)
            ? File.ReadAllText(path)
            : null;
    }

    public void MoveFile(string sourcePath, string targetPath)
    {
        var directory = Path.GetDirectoryName(targetPath);
        Directory.CreateDirectory(directory);
        File.Move(sourcePath, targetPath);
    }

    public void SaveText(string path, string text)
    {
        var directory = Path.GetDirectoryName(path);
        Directory.CreateDirectory(directory);
        File.WriteAllText(path, text);
    }
}
