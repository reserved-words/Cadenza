using Cadenza.API.Common.Model;
using Cadenza.API.Common.Repositories;
using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Interfaces.Converters;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Database;

internal class MusicRepository : IMusicRepository
{
    private readonly IAlbumConverter _albumConverter;
    private readonly IArtistConverter _artistConverter;
    private readonly ITrackConverter _trackConverter;
    private readonly IDataAccess _dataAccess;
    private readonly IJsonToModelConverter _converter;

    public MusicRepository(IDataAccess dataAccess, IJsonToModelConverter converter, IArtistConverter artistConverter,
        IAlbumConverter albumConverter, ITrackConverter trackConverter)
    {
        _dataAccess = dataAccess;
        _converter = converter;
        _artistConverter = artistConverter;
        _albumConverter = albumConverter;
        _trackConverter = trackConverter;
    }

    public async Task<FullLibrary> Get()
    {
        var jsonArtists = await _dataAccess.GetArtists();

        var library = new FullLibrary();

        AddArtists(library, jsonArtists);

        var sources = Enum.GetValues<LibrarySource>();

        foreach (var source in sources)
        {
            await AddSource(library, jsonArtists, source);
        }

        return library;
    }

    public async Task<FullLibrary> Get(LibrarySource source)
    {
        var jsonArtists = await _dataAccess.GetArtists();

        var library = new FullLibrary();

        AddArtists(library, jsonArtists);

        await AddSource(library, jsonArtists, source);

        return library;
    }

    public async Task UpdateAlbum(AlbumInfo album)
    {
        var albums = await _dataAccess.GetAlbums(album.Source);

        var jsonAlbum = albums.SingleOrDefault(a => a.Id == album.Id);
        if (jsonAlbum == null)
            return;

        albums.Remove(jsonAlbum);
        var artists = await _dataAccess.GetArtists();
        jsonAlbum = _albumConverter.ToJson(album);
        albums.Add(jsonAlbum);
        await _dataAccess.SaveAlbums(albums, album.Source);
    }

    public async Task UpdateArtist(ArtistInfo artist)
    {
        var artists = await _dataAccess.GetArtists();
        var jsonArtist = artists.SingleOrDefault(a => a.Id == artist.Id);
        if (jsonArtist == null)
            return;

        artists.Remove(jsonArtist);
        jsonArtist = _artistConverter.ToJson(artist);
        artists.Add(jsonArtist);
        await _dataAccess.SaveArtists(artists);
    }

    public async Task UpdateTrack(TrackInfo track)
    {
        var tracks = await _dataAccess.GetTracks(track.Source);

        var jsonTrack = tracks.SingleOrDefault(a => a.Id == track.Id);
        if (jsonTrack == null)
            return;

        tracks.Remove(jsonTrack);
        var artists = await _dataAccess.GetArtists();
        jsonTrack = _trackConverter.ToJson(track);
        tracks.Add(jsonTrack);
        await _dataAccess.SaveTracks(tracks, track.Source);
    }

    private void AddArtists(FullLibrary library, List<JsonArtist> jsonArtists)
    {
        foreach (var jsonArtist in jsonArtists)
        {
            var artist = _converter.ConvertArtist(jsonArtist);
            library.Artists.Add(artist);
        }
    }

    private async Task AddSource(FullLibrary library, List<JsonArtist> jsonArtists, LibrarySource source)
    {
        var jsonAlbumTrackLinks = await _dataAccess.GetAlbumTrackLinks(source);

        foreach (var jsonAlbumTrackLink in jsonAlbumTrackLinks)
        {
            var albumTrack = _converter.ConvertAlbumTrackLink(jsonAlbumTrackLink);
            library.AlbumTrackLinks.Add(albumTrack);
        }

        var jsonTracks = await _dataAccess.GetTracks(source);

        foreach (var jsonTrack in jsonTracks)
        {
            var track = _converter.ConvertTrack(jsonTrack, jsonArtists);
            track.Source = source;
            library.Tracks.Add(track);
        }

        var jsonAlbums = await _dataAccess.GetAlbums(source);

        foreach (var jsonAlbum in jsonAlbums)
        {
            var album = _converter.ConvertAlbum(jsonAlbum, jsonArtists);
            album.Source = source;
            library.Albums.Add(album);
        }
    }
}
