using Cadenza.Common.Domain.Model.Sync;

namespace Cadenza.Local.API.Core;

internal class SyncService : ISyncService
{
    private readonly IFilepathParser _filepathParser;
    private readonly IMusicDirectory _musicDirectory;
    private readonly IMusicFilesService _musicLibrary;

    public SyncService(IMusicDirectory musicDirectory, IMusicFilesService musicLibrary, IFilepathParser filepathParser)
    {
        _musicDirectory = musicDirectory;
        _musicLibrary = musicLibrary;
        _filepathParser = filepathParser;
    }

    public async Task<List<string>> GetAllTracks()
    {
        var files = await _musicDirectory.GetAllFiles();
        return files
            .Select(f => _filepathParser.GetIdFromFilepath(f.Path))
            .ToList();
    }

    public async Task<SyncTrack> GetTrack(string id)
    {
        var filepath = _filepathParser.GetFilepathFromId(id);
        var data = await _musicLibrary.GetFileData(id, filepath);
        return data;
    }

    public async Task RemoveTrack(string id)
    {
        var filepath = _filepathParser.GetFilepathFromId(id);
        await _musicDirectory.RemoveFile(filepath);
    }

    public async Task UpdateTracks(MultiTrackUpdates updates)
    {
        foreach (var id in updates.TrackIds)
        {
            var filepath = _filepathParser.GetFilepathFromId(id);
            await _musicLibrary.UpdateFileData(filepath, updates.Updates);
        }
    }
}
