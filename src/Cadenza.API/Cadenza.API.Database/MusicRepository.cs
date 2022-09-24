using Cadenza.API.Common.Repositories;
using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Model;
using Cadenza.Domain;
using Microsoft.Extensions.Configuration;

namespace Cadenza.API.Database;

internal class MusicRepository : IMusicRepository
{
    private readonly IAlbumConverter _albumConverter;
    private readonly IArtistConverter _artistConverter;
    private readonly ITrackConverter _trackConverter;
    private readonly IConfiguration _config;
    private readonly IDataAccess _dataAccess;
    private readonly IJsonToModelConverter _converter;

    // Note this is only set up for local source, need to change this to allow fetching from all sources

    public MusicRepository(IDataAccess dataAccess, IJsonToModelConverter converter, IConfiguration config, IArtistConverter artistConverter,
        IAlbumConverter albumConverter, ITrackConverter trackConverter)
    {
        _dataAccess = dataAccess;
        _converter = converter;
        _config = config;
        _artistConverter = artistConverter;
        _albumConverter = albumConverter;
        _trackConverter = trackConverter;
    }

    public async Task<FullLibrary> Get()
    {
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
            // For some sources can populate this, if not populated the Blazor app should know how to fetch artwork using the ID
            //album.ArtworkUrl = string.Format(artworkUrlFormat, firstTracks[album.Id]);
            library.Albums.Add(album);
        }

        foreach (var source in Enum.GetValues<LibrarySource>())
        {
            if (source == LibrarySource.Local)
                continue;

            await AddSource(library, source, jsonArtists);
        }

        return library;
    }

    private async Task AddSource(FullLibrary library, LibrarySource source, List<JsonArtist> jsonArtists)
    {
        var jsonAlbums = await _dataAccess.GetAlbums(source);
        var jsonTracks = await _dataAccess.GetTracks(source);
        var jsonAlbumTrackLinks = await _dataAccess.GetAlbumTrackLinks(source);

        foreach (var jsonAlbumTrackLink in jsonAlbumTrackLinks)
        {
            var albumTrack = new AlbumTrackLink
            {
                AlbumId = jsonAlbumTrackLink.AlbumId,
                TrackId = jsonAlbumTrackLink.TrackId,
                Position = new AlbumTrackPosition(jsonAlbumTrackLink.DiscNo ?? 1, jsonAlbumTrackLink.TrackNo)
            };
            library.AlbumTrackLinks.Add(albumTrack);
        }

        foreach (var jsonTrack in jsonTracks)
        {
            var track = _converter.ConvertTrack(jsonTrack, jsonArtists);
            track.Source = source;
            library.Tracks.Add(track);
        }

        foreach (var jsonAlbum in jsonAlbums)
        {
            var album = _converter.ConvertAlbum(jsonAlbum, jsonArtists);
            album.Source = source;
            library.Albums.Add(album);
        }
    }

    public async Task UpdateAlbum(AlbumInfo album)
    {
        var albums = await _dataAccess.GetAlbums(album.Source);

        var jsonAlbum = albums.SingleOrDefault(a => a.Id == album.Id);
        if (jsonAlbum == null)
            return;

        albums.Remove(jsonAlbum);
        var artists = await _dataAccess.GetArtists();
        jsonAlbum = _albumConverter.ToJsonModel(album);
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
        jsonArtist = _artistConverter.ToJsonModel(artist);
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
        jsonTrack = _trackConverter.ToJsonModel(track);
        tracks.Add(jsonTrack);
        await _dataAccess.SaveTracks(tracks, track.Source);
    }
}
