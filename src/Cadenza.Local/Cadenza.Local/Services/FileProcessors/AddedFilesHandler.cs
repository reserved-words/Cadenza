using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.FileProcessors;

namespace Cadenza.Local.Services.FileProcessors;

public class AddedFilesHandler : IAddedFilesHandler
{
    private readonly IUpdatedFilesFetcher _fileFetcher;
    private readonly IDataAccess _dataAccess;
    private readonly ILibraryOrganiser _organiser;
    private readonly IMusicFileLibrary _musicFiles;

    public AddedFilesHandler(ILibraryOrganiser organiser, IDataAccess dataAccess, IUpdatedFilesFetcher fileFetcher, IMusicFileLibrary musicFiles)
    {
        _organiser = organiser;
        _dataAccess = dataAccess;
        _fileFetcher = fileFetcher;
        _musicFiles = musicFiles;
    }

    public async Task Sync()
    {
        var jsonItems = await _dataAccess.GetAll(LibrarySource.Local);

        var filepaths = await _fileFetcher.GetAddedFiles();

        foreach (var filePath in filepaths)
        {
            Console.WriteLine($"Adding file {filePath}");

            var data = _musicFiles.GetFileData(filePath);

            _organiser.MergeArtist(jsonItems.Artists, data.TrackArtist);
            if (data.AlbumArtist.Id != data.TrackArtist.Id)
            {
                _organiser.MergeArtist(jsonItems.Artists, data.AlbumArtist);
            }

            _organiser.MergeTrack(jsonItems.Tracks, jsonItems.Artists, data.Track);
            _organiser.MergeAlbum(jsonItems.Albums, jsonItems.Artists, data.Album);
            _organiser.MergeAlbumTrackLink(jsonItems.AlbumTrackLinks, data.AlbumTrackLink);
        }

        _organiser.RemoveOrphanedItems(jsonItems);

        await _dataAccess.SaveAll(jsonItems, LibrarySource.Local);
    }
}