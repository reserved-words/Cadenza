using Cadenza.Library;
using Microsoft.Extensions.Configuration;

namespace Cadenza.Local;

public class JsonLibrary : IStaticLibrary
{
    private readonly IArtistConverter _artistConverter;
    private readonly IConfiguration _config;
    private readonly IDataAccess _dataAccess;
    private readonly IJsonToModelConverter _converter;

    public JsonLibrary(IDataAccess dataAccess, IJsonToModelConverter converter, IConfiguration config, IArtistConverter artistConverter)
    {
        _dataAccess = dataAccess;
        _converter = converter;
        _config = config;
        _artistConverter = artistConverter;
    }

    public async Task<FullLibrary> Get()
    {
        var artworkUrlFormat = _config.GetValue<string>("ArtworkUrl");

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
            album.ArtworkUrl = string.Format(artworkUrlFormat, firstTracks[album.Id]);
            library.Albums.Add(album);
        }

        return library;
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var artists = await _dataAccess.GetArtists();
        var jsonArtist = artists.SingleOrDefault(a => a.Id == update.Id);
        if (jsonArtist == null)
            return;

        artists.Remove(jsonArtist);
        var artist = _artistConverter.ToAppModel(jsonArtist);
        update.ApplyUpdates(artist);
        jsonArtist = _artistConverter.ToJsonModel(artist);
        artists.Add(jsonArtist);
        await _dataAccess.SaveArtists(artists);
    }
}
