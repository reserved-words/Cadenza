using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.FileProcessors;

namespace Cadenza.Local.Services.FileProcessors;

public class ModifiedFilesHandler : IModifiedFilesHandler
{
    private readonly IUpdatedFilesFetcher _fileFetcher;
    private readonly IDataAccess _dataAccess;
    private readonly ILibraryOrganiser _organiser;
    private readonly IMusicFileLibrary _musicFileLibrary;

    public ModifiedFilesHandler(ILibraryOrganiser organiser, IDataAccess dataAccess, IUpdatedFilesFetcher fileFetcher, IMusicFileLibrary musicFileLibrary)
    {
        _organiser = organiser;
        _dataAccess = dataAccess;
        _fileFetcher = fileFetcher;
        _musicFileLibrary = musicFileLibrary;
    }

    public async Task Sync()
    {
        var jsonItems = await _dataAccess.GetAll(LibrarySource.Local);

        var updatesFetched = DateTime.Now;

        var filepaths = await _fileFetcher.GetModifiedFiles();

        foreach (var filepath in filepaths)
        {
            Console.WriteLine($"Modifying file {filepath}");

            var data = _musicFileLibrary.GetFileData(filepath);

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

        await _fileFetcher.UpdateTimeModifiedFilesUpdated(updatesFetched);
    }
}