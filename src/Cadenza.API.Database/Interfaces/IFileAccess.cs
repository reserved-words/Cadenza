namespace Cadenza.API.Common.Interfaces;

internal interface IFileAccess
{
    Task<string> GetText(string path);
    Task SaveText(string path, string text);
}
