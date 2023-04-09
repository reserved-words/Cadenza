using Cadenza.Common.Domain.Model.Sync;

namespace Cadenza.Local.API.Core;

internal class SyncService : ISyncService
{
    private readonly IMusicDirectory _musicDirectory;
    private readonly IBase64Converter _base64Converter;
    private readonly IMusicFilesService _musicLibrary;

    public SyncService(IMusicDirectory musicDirectory, IBase64Converter base64Converter, IMusicFilesService musicLibrary)
    {
        _musicDirectory = musicDirectory;
        _base64Converter = base64Converter;
        _musicLibrary = musicLibrary;
    }

    public async Task<List<string>> GetAllTracks()
    {
        var files = await _musicDirectory.GetAllFiles();
        return files
            .Select(f => f.Path)
            .Select(p => _base64Converter.ToBase64(p))
            .ToList();
    }

    public async Task<SyncTrack> GetTrack(string id)
    {
        var path = _base64Converter.FromBase64(id);
        return await _musicLibrary.GetFileData(path);
    }

    public async Task UpdateTracks(MultiTrackUpdates updates)
    {
        foreach (var trackId in updates.TrackIds)
        {
            var path = _base64Converter.FromBase64(trackId);
            await _musicLibrary.UpdateFileData(path, updates.Updates);
        }
    }
}
