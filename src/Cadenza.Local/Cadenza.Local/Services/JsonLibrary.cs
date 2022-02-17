using Cadenza.Library;

namespace Cadenza.Local;

public class JsonLibrary : IStaticLibrary
{
    private const string ArtworkUrlFormat = "/library/artwork?id={0}";

    private readonly IDataAccess _dataAccess;
    private readonly IJsonToModelConverter _converter;

    public JsonLibrary(IDataAccess dataAccess, IJsonToModelConverter converter)
    {
        _dataAccess = dataAccess;
        _converter = converter;
    }

    public async Task<FullLibrary> Get()
    {
        var jsonArtists = await _dataAccess.GetArtists();
        var jsonAlbums = await _dataAccess.GetAlbums();
        var jsonTracks = await _dataAccess.GetTracks();
        var jsonAlbumTrackLinks = await _dataAccess.GetAlbumTrackLinks();

        var library = new FullLibrary();

        var firstTracks = new Dictionary<string, string>();

        foreach (var jsonAlbumTrackLink in jsonAlbumTrackLinks)
        {
            var albumTrack = _converter.ConvertAlbumTrackLink(jsonAlbumTrackLink);
            library.AlbumTrackLinks.Add(albumTrack);
            firstTracks.TryAdd(albumTrack.AlbumId, albumTrack.TrackId);
        }

        foreach (var jsonArtist in jsonArtists)
        {
            var artist = _converter.ConvertArtist(jsonArtist);
            library.Artists.Add(artist);
        }

        foreach (var jsonTrack in jsonTracks)
        {
            var track = _converter.ConvertTrack(jsonTrack, jsonArtists);
            track.Source = LibrarySource.Local;
            library.Tracks.Add(track);
        }

        foreach (var jsonAlbum in jsonAlbums)
        {
            var album = _converter.ConvertAlbum(jsonAlbum, jsonArtists);
            album.Source = LibrarySource.Local;
            album.ArtworkUrl = string.Format(ArtworkUrlFormat, firstTracks[album.Id]);
            library.Albums.Add(album);
        }

        return library;
    }
}
