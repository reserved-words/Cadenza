using Cadenza.Local.Common.Interfaces;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.Local.SyncService.Updaters;

public class DeletedFilesHandler : IUpdateService
{
    private readonly IBase64Converter _base64Converter;
    private readonly IDatabaseRepository _repository;
    private readonly IMusicDirectory _musicDirectory;

    public DeletedFilesHandler(IMusicDirectory musicDirectory, IDatabaseRepository repository, IBase64Converter base64Converter)
    {
        _musicDirectory = musicDirectory;
        _repository = repository;
        _base64Converter = base64Converter;
    }

    public async Task Run()
    {
        var remoteFileIds = await _repository.GetAllTracks();

        var localFiles = await _musicDirectory.GetAllFiles();
        var localTrackIds = localFiles.Select(f => _base64Converter.ToBase64(f.Path));

        var removedTrackIds = remoteFileIds.Except(localTrackIds);

        foreach (var trackId in removedTrackIds)
        {
            await _repository.RemoveTrack(trackId);
        }

        //var jsonData = await _dataAccess.GetAll(LibrarySource.Local);

        //_organiser.RemoveTracks(jsonData, filepaths);

        //await _dataAccess.SaveAll(jsonData, LibrarySource.Local);
    }
}