namespace Cadenza.Local;

public class ModifiedFilesHandler : IModifiedFilesHandler
{
    private readonly IUpdatedFilesFetcher _fileFetcher;
    private readonly IId3TagsService _id3Service;

    private readonly IDataAccess _dataAccess;
    private readonly IId3ToJsonConverter _converter;
    private readonly ILibraryOrganiser _organiser;

    public ModifiedFilesHandler(IId3TagsService id3Service, ILibraryOrganiser organiser, IId3ToJsonConverter converter,
        IDataAccess dataAccess, IUpdatedFilesFetcher fileFetcher)
    {
        _id3Service = id3Service;
        _organiser = organiser;
        _converter = converter;
        _dataAccess = dataAccess;
        _fileFetcher = fileFetcher;
    }

    public void Sync()
    {
        var jsonItems = _dataAccess.GetAll();

        var updatesFetched = DateTime.Now;

        var filepaths = _fileFetcher.GetFilesModifiedSinceLastUpdate();

        foreach (var filePath in filepaths)
        {
            Console.WriteLine($"Modifying file {filePath}");

            var id3Track = _id3Service.GetId3Data(filePath);

            var newTrackArtist = _converter.ConvertTrackArtist(id3Track);
            _organiser.MergeArtist(jsonItems.Artists, newTrackArtist);

            var newAlbumArtist = _converter.ConvertAlbumArtist(id3Track);
            if (newAlbumArtist.Id != newTrackArtist.Id)
            {
                _organiser.MergeArtist(jsonItems.Artists, newAlbumArtist);
            }

            var newTrack = _converter.ConvertTrack(id3Track);
            _organiser.MergeTrack(jsonItems.Tracks, jsonItems.Artists, newTrack);

            var newAlbum = _converter.ConvertAlbum(id3Track);
            _organiser.MergeAlbum(jsonItems.Albums, jsonItems.Artists, newAlbum);

            var newAlbumTrackLink = _converter.ConvertAlbumTrackLink(id3Track);
            _organiser.MergeAlbumTrackLink(jsonItems.AlbumTrackLinks, newAlbumTrackLink);
        }

        _organiser.RemoveOrphanedItems(jsonItems);

        _dataAccess.SaveAll(jsonItems);

        _fileFetcher.UpdateTimeModifiedFilesUpdated(updatesFetched);
    }
}