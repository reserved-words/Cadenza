using Cadenza.Common.Domain.Model.Sync;

namespace Cadenza.Local.API.Core;

internal class SyncService : ISyncService
{
    private readonly IMusicDirectory _musicDirectory;
    private readonly IMusicFilesService _musicLibrary;

    public SyncService(IMusicDirectory musicDirectory, IMusicFilesService musicLibrary)
    {
        _musicDirectory = musicDirectory;
        _musicLibrary = musicLibrary;
    }

    public async Task<List<string>> GetAllTracks()
    {
        var files = await _musicDirectory.GetAllFiles();
        return files
            .Select(f => f.Path)
            .ToList();
    }

    public async Task<SyncTrack> GetTrack(string id)
    {
        return await _musicLibrary.GetFileData(id);
    }

    public async Task RemoveTrack(string id)
    {
        await _musicDirectory.RemoveFile(id);
    }

    public async Task UpdateTracks(MultiTrackUpdates updates)
    {
        foreach (var id in updates.TrackIds)
        {
            await _musicLibrary.UpdateFileData(id, updates.Updates);
        }
    }
}
