using Cadenza.API.Common.Interfaces;
using Cadenza.Domain;
using Cadenza.Utilities;

namespace Cadenza.API.Database.Services;

public class LocalFileUpdater : ILocalFilesUpdater
{
    private readonly IBase64Converter _base64Converter;
    private readonly IMusicFileLibrary _musicFileFilbrary;
    private readonly IDataAccess _dataAccess;

    public LocalFileUpdater(IDataAccess dataAccess, IBase64Converter base64Converter, IMusicFileLibrary musicFileFilbrary)
    {
        _dataAccess = dataAccess;
        _base64Converter = base64Converter;
        _musicFileFilbrary = musicFileFilbrary;
    }

    public async Task UpdateAlbum(string id, List<ItemPropertyUpdate> updates)
    {
        var tracks = await GetAlbumTrackPaths(id);
        UpdateTracks(tracks, updates);
    }

    public async Task UpdateArtist(string id, List<ItemPropertyUpdate> updates)
    {
        var tracks = await GetArtistTrackPaths(id);
        UpdateTracks(tracks, updates);
    }

    public Task UpdateTrack(string id, List<ItemPropertyUpdate> updates)
    {
        var path = _base64Converter.FromBase64(id);
        UpdateTracks(new List<string> { path }, updates);
        return Task.CompletedTask;
    }

    private async Task<List<string>> GetAlbumTrackPaths(string id)
    {
        var tracks = await _dataAccess.GetTracks(LibrarySource.Local);
        return tracks
            .Where(t => t.AlbumId == id)
            .Select(t => t.Path)
            .ToList();
    }

    private async Task<List<string>> GetArtistTrackPaths(string id)
    {
        var tracks = await _dataAccess.GetTracks(LibrarySource.Local);
        return tracks
            .Where(t => t.ArtistId == id)
            .Select(t => t.Path)
            .ToList();
    }

    private void UpdateTracks(List<string> paths, List<ItemPropertyUpdate> updates)
    {
        foreach (var path in paths)
        {
            _musicFileFilbrary.UpdateFileData(path, updates);
        }
    }
}