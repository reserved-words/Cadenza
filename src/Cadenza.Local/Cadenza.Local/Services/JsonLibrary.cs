using Cadenza.Library;
using Microsoft.Extensions.Configuration;

namespace Cadenza.Local;

public class JsonLibrary : IStaticLibrary
{
    private readonly IAlbumConverter _albumConverter;
    private readonly IArtistConverter _artistConverter;
    private readonly IBase64Converter _base64Converter;
    private readonly ITrackConverter _trackConverter;
    private readonly IConfiguration _config;
    private readonly IDataAccess _dataAccess;
    private readonly IJsonToModelConverter _converter;

    public JsonLibrary(IDataAccess dataAccess, IJsonToModelConverter converter, IConfiguration config, IArtistConverter artistConverter, 
        IAlbumConverter albumConverter, ITrackConverter trackConverter, IBase64Converter base64Converter)
    {
        _dataAccess = dataAccess;
        _converter = converter;
        _config = config;
        _artistConverter = artistConverter;
        _albumConverter = albumConverter;
        _trackConverter = trackConverter;
        _base64Converter = base64Converter;
    }

    public async Task<FullLibrary> Get()
    {
        var artworkUrlFormat = _config.GetValue<string>("ArtworkUrl");

        var jsonArtists = await _dataAccess.GetArtists();
        var jsonAlbums = await _dataAccess.GetAlbums(LibrarySource.Local);
        var jsonTracks = await _dataAccess.GetTracks(LibrarySource.Local);
        var jsonAlbumTrackLinks = await _dataAccess.GetAlbumTrackLinks(LibrarySource.Local);

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

    public async Task UpdateAlbum(AlbumUpdate update)
    {
        var albums = await _dataAccess.GetAlbums(update.UpdatedItem.Source);

        var jsonAlbum = albums.SingleOrDefault(a => a.Id == update.Id);
        if (jsonAlbum == null)
            return;

        albums.Remove(jsonAlbum);
        var artists = await _dataAccess.GetArtists();
        var album = _albumConverter.ToAppModel(jsonAlbum, artists);
        update.ApplyUpdates(album);
        jsonAlbum = _albumConverter.ToJsonModel(album);
        albums.Add(jsonAlbum);
        await _dataAccess.SaveAlbums(albums, update.UpdatedItem.Source);
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

    public async Task UpdateTrack(TrackUpdate update)
    {
        var tracks = await _dataAccess.GetTracks(update.UpdatedItem.Source);
        var jsonTrack = tracks.SingleOrDefault(a => a.Path == _base64Converter.FromBase64(update.Id));
        if (jsonTrack == null)
            return;

        tracks.Remove(jsonTrack);
        var artists = await _dataAccess.GetArtists();
        var track = _trackConverter.ToAppModel(jsonTrack, artists);
        update.ApplyUpdates(track);
        jsonTrack = _trackConverter.ToJsonModel(track);
        tracks.Add(jsonTrack);
        await _dataAccess.SaveTracks(tracks, update.UpdatedItem.Source);
    }
}
