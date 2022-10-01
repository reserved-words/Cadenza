namespace Cadenza.API.Database.Services;

internal class FileDataService : IFileDataService
{
    private readonly IFileAccess _fileAccess;
    private readonly IJsonConverter _jsonConverter;

    public FileDataService(IFileAccess fileAccess, IJsonConverter jsonConverter)
    {
        _fileAccess = fileAccess;
        _jsonConverter = jsonConverter;
    }

    public Task Save<T>(string path, T data)
    {
        var json = _jsonConverter.Serialize(data);
        _fileAccess.SaveText(path, json);
        return Task.CompletedTask;
    }

    public Task<T> Get<T>(string path) where T : class, new()
    {
        var json = _fileAccess.GetText(path);
        var result = _jsonConverter.Deserialize<T>(json);
        return Task.FromResult(result);
    }
}
