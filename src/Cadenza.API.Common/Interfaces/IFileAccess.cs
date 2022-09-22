namespace Cadenza.API.Common.Interfaces;

public interface IFileAccess
{
    Task<string> GetText(string path);
    Task SaveText(string path, string text);
}
