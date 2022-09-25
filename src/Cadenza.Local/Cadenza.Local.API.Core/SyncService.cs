using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;
using Cadenza.Local.API.Common.Controllers;
using Cadenza.Local.API.Common.Interfaces;
using Cadenza.Local.API.Core.Interfaces;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.Local.API.Core;

internal class SyncService : ISyncService
{
    private readonly IMusicDirectory _musicDirectory;
    private readonly IBase64Converter _base64Converter;
    private readonly IMusicFileLibrary _musicLibrary;

    public SyncService(IMusicDirectory musicDirectory, IBase64Converter base64Converter, IMusicFileLibrary musicLibrary)
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

    public Task UpdateTrack(string id, List<PropertyUpdate> updates)
    {
        var path = _base64Converter.FromBase64(id);
        _musicLibrary.UpdateFileData(path, updates);
        return Task.CompletedTask;
    }
}
