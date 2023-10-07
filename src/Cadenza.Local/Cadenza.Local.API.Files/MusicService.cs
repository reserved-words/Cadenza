using Cadenza.Common.Domain.Model.Sync;
using Cadenza.Common.DTO;

namespace Cadenza.Local.API.Files;

internal class MusicService : IMusicFilesService
{
    private readonly IId3Fetcher _fetcher;
    private readonly IId3Updater _updater;

    public MusicService(IId3Updater updater, IId3Fetcher fetcher)
    {
        _updater = updater;
        _fetcher = fetcher;
    }

    public Task<SyncTrackDTO> GetFileData(string id, string filepath)
    {
        var result = _fetcher.GetFileData(id, filepath);
        return Task.FromResult(result);
    }

    public Task UpdateFileData(string filepath, List<PropertyUpdateDTO> updates)
    {
        _updater.UpdateTags(filepath, updates);
        return Task.CompletedTask;
    }
}