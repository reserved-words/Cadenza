using Cadenza.Domain;
using Cadenza.Local.Common.Interfaces;

namespace Cadenza.Local.SyncService.Updaters;

public class ModifiedFilesHandler : IUpdateService
{
    private readonly IUpdatedFilesFetcher _fileFetcher;
    private readonly IMusicFileLibrary _musicFileLibrary;

    public ModifiedFilesHandler(IUpdatedFilesFetcher fileFetcher, IMusicFileLibrary musicFileLibrary)
    {
        _fileFetcher = fileFetcher;
        _musicFileLibrary = musicFileLibrary;
    }

    public Task Run()
    {
        throw new NotImplementedException();

        //var jsonItems = await _dataAccess.GetAll(LibrarySource.Local);

        //var updatesFetched = DateTime.Now;

        //var filepaths = await _fileFetcher.GetModifiedFiles();

        //foreach (var filepath in filepaths)
        //{
        //    Console.WriteLine($"Modifying file {filepath}");

        //    var data = _musicFileLibrary.GetFileData(filepath);

        //    _organiser.MergeArtist(jsonItems.Artists, data.TrackArtist);

        //    if (data.AlbumArtist.Id != data.TrackArtist.Id)
        //    {
        //        _organiser.MergeArtist(jsonItems.Artists, data.AlbumArtist);
        //    }

        //    _organiser.MergeTrack(jsonItems.Tracks, jsonItems.Artists, data.Track);
        //    _organiser.MergeAlbum(jsonItems.Albums, jsonItems.Artists, data.Album);
        //    _organiser.MergeAlbumTrackLink(jsonItems.AlbumTrackLinks, data.AlbumTrackLink);
        //}

        //_organiser.RemoveOrphanedItems(jsonItems);

        //await _dataAccess.SaveAll(jsonItems, LibrarySource.Local);

        //await _fileFetcher.UpdateTimeModifiedFilesUpdated(updatesFetched);
    }
}