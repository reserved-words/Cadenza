namespace Cadenza.API.JsonLibrary.Interfaces;

internal interface IFileDataService
{
    Task Save<T>(string path, T data);
    Task<T> Get<T>(string path) where T : class, new();
}