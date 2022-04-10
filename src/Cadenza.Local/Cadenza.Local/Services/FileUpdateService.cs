using Cadenza.Domain;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Model;
using Microsoft.Extensions.Options;

namespace Cadenza.Local;

public class FileUpdateService : IFileUpdateService
{
    private readonly IFileAccess _fileAccess;
    private readonly IJsonConverter _jsonConverter;
    private readonly IOptions<LibraryPaths> _config;

    public FileUpdateService(IFileAccess fileAccess, IJsonConverter jsonConverter, IOptions<LibraryPaths> config)
    {
        _config = config;
        _fileAccess = fileAccess;
        _jsonConverter = jsonConverter;
    }

    public async Task Add(ItemPropertyUpdate update)
    {
        var queue = await Get();
        AddOrUpdate(queue, update);
        Save(queue);
    }

    private void AddOrUpdate(FileUpdateQueue queue, ItemPropertyUpdate update)
    {
        var entry = queue.Updates.SingleOrDefault(e => e.Update.Equals(update));
        if (entry == null)
        {
            entry = new FileUpdate { Update = update };
            queue.Updates.Add(entry);
        }
        entry.FailedAttempts.Clear();
    }

    public async Task Remove(ItemPropertyUpdate update)
    {
        var queue = await Get();

        var existing = queue.Updates.SingleOrDefault(e => e.Update.Equals(update));

        if (existing == null)
            return;

        queue.Updates.Remove(existing);
        Save(queue);
    }

    public async Task LogError(ItemPropertyUpdate update, Exception ex)
    {
        var queue = await Get();

        var savedUpdate = queue.Updates.SingleOrDefault(e => e.Update.Equals(update));

        savedUpdate.FailedAttempts.Add(new FileUpdateFailedAttempt
        {
            Date = DateTime.Now,
            Error = ex.Message
        });

        Save(queue);
    }

    public async Task<FileUpdateQueue> Get()
    {
        var path = GetUpdateQueuePath();
        var json = await _fileAccess.GetText(path);
        var queue = _jsonConverter.Deserialize<FileUpdateQueue>(json);
        return queue ?? new FileUpdateQueue();
    }

    private void Save(FileUpdateQueue queue)
    {
        var path = GetUpdateQueuePath();
        var json = _jsonConverter.Serialize(queue);
        _fileAccess.SaveText(path, json);
    }

    private string GetUpdateQueuePath()
    {
        var directory = _config.Value.BaseDirectory;
        return Path.Combine(directory, _config.Value.UpdateQueue);
    }
}