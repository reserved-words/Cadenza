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

    public Task<TrackFull> GetTrack(string id)
    {
        var path = _base64Converter.FromBase64(id);
        var track = _musicLibrary.GetFileData(path);
        return Task.FromResult(track);
    }

    public Task UpdateTracks(MultiTrackUpdates updates)
    {
        foreach (var trackId in updates.TrackIds)
        {
            var path = _base64Converter.FromBase64(trackId);
            _musicLibrary.UpdateFileData(path, updates.Updates);
        }

        return Task.CompletedTask;
    }
}
