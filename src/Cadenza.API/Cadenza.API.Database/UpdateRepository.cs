//using Cadenza.API.Common.Interfaces;
//using Cadenza.API.Database.Interfaces;
//using Cadenza.Domain;
//using Cadenza.Utilities;
//using Microsoft.Extensions.Options;

//namespace Cadenza.API.Database.Services;

//internal class UpdateRepository : IUpdateRepository
//{
//    private readonly IFileAccess _fileAccess;
//    private readonly IJsonConverter _jsonConverter;
//    private readonly IOptions<LibraryPaths> _config;

//    public UpdateRepository(IFileAccess fileAccess, IJsonConverter jsonConverter, IOptions<LibraryPaths> config)
//    {
//        _config = config;
//        _fileAccess = fileAccess;
//        _jsonConverter = jsonConverter;
//    }

//    public async Task Add(ItemPropertyUpdate update)
//    {
//        var queue = await Get();
//        AddOrUpdate(queue, update);
//        Save(queue);
//    }

//    private void AddOrUpdate(List<ItemPropertyUpdate> queue, ItemPropertyUpdate update)
//    {
//        var entry = queue.SingleOrDefault(e => e.Equals(update));
//        if (entry != null)
//        {
//            queue.Remove(entry);
//        }
//        queue.Add(update);
//    }

//    public async Task Remove(ItemPropertyUpdate update)
//    {
//        var queue = await Get();

//        var existing = queue.SingleOrDefault(e => e.Equals(update));

//        if (existing == null)
//            return;

//        queue.Remove(existing);
//        Save(queue);
//    }

//    //public async Task LogError(ItemPropertyUpdate update, Exception ex)
//    //{
//    //    var queue = await Get();

//    //    var savedUpdate = queue.Updates.SingleOrDefault(e => e.Update.Equals(update));

//    //    savedUpdate.FailedAttempts.Add(new FileUpdateFailedAttempt
//    //    {
//    //        Date = DateTime.Now,
//    //        Error = ex.Message
//    //    });

//    //    Save(queue);
//    //}

//    public async Task<List<ItemPropertyUpdate>> Get()
//    {
//        var path = GetUpdateQueuePath();
//        var json = await _fileAccess.GetText(path);
//        var queue = _jsonConverter.Deserialize<List<ItemPropertyUpdate>>(json);
//        return queue ?? new List<ItemPropertyUpdate>();
//    }

//    private void Save(List<ItemPropertyUpdate> queue)
//    {
//        var path = GetUpdateQueuePath();
//        var json = _jsonConverter.Serialize(queue);
//        _fileAccess.SaveText(path, json);
//    }

//    private string GetUpdateQueuePath()
//    {
//        var directory = _config.Value.BaseDirectory;
//        return Path.Combine(directory, _config.Value.UpdateQueue);
//    }
//}