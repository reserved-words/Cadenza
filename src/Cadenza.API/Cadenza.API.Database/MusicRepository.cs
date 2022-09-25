using Cadenza.API.Common.Model;
using Cadenza.API.Common.Repositories;
using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;

namespace Cadenza.API.Database;

internal class MusicRepository : IMusicRepository
{
    private readonly IDataAccess _dataAccess;
    private readonly IJsonToModelConverter _converter;

    public MusicRepository(IDataAccess dataAccess, IJsonToModelConverter converter)
    {
        _dataAccess = dataAccess;
        _converter = converter;
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

    public async Task UpdateAlbum(LibrarySource source, ItemUpdates updates)
    {
        var albums = await _dataAccess.GetAlbums(source);

        var album = albums.SingleOrDefault(a => a.Id == updates.Id);

        if (album == null)
            return;

        UpdateAlbum(album, updates.Updates);

        await _dataAccess.SaveAlbums(albums, source);
    }

    public async Task UpdateArtist(ItemUpdates updates)
    {
        var artists = await _dataAccess.GetArtists();

        var artist = artists.SingleOrDefault(a => a.Id == updates.Id);

        if (artist == null)
            return;

        UpdateArtist(artist, updates.Updates);

        await _dataAccess.SaveArtists(artists);
    }

    public async Task UpdateTrack(LibrarySource source, ItemUpdates updates)
    {
        var tracks = await _dataAccess.GetTracks(source);

        var track = tracks.SingleOrDefault(a => a.Id == updates.Id);

        if (track == null)
            return;

        UpdateTrack(track, updates.Updates);

        await _dataAccess.SaveTracks(tracks, source);
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

    private static void UpdateAlbum(JsonAlbum album, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
                case ItemProperty.AlbumTitle:
                    album.Title = update.UpdatedValue;
                    break;
                case ItemProperty.ReleaseType:
                    album.ReleaseType = update.UpdatedValue;
                    break;
                case ItemProperty.ReleaseYear:
                    album.Year = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            } 
        }
    }

    private static void UpdateTrack(JsonTrack track, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
                case ItemProperty.Lyrics:
                    track.Lyrics = update.UpdatedValue;
                    break;
                case ItemProperty.TrackTitle:
                    track.Title = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            } 
        }
    }

    private static void UpdateArtist(JsonArtist artist, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
                case ItemProperty.City:
                    artist.City = update.UpdatedValue;
                    break;
                case ItemProperty.Country:
                    artist.Country = update.UpdatedValue;
                    break;
                case ItemProperty.Genre:
                    artist.Genre = update.UpdatedValue;
                    break;
                case ItemProperty.Grouping:
                    artist.Grouping = update.UpdatedValue;
                    break;
                case ItemProperty.State:
                    artist.State = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            } 
        }
    }
}
