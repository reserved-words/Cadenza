using Cadenza.API.Common.Model;
using Cadenza.API.Common.Repositories;
using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Track;
using Cadenza.Domain.Models.Updates;

namespace Cadenza.API.Database;

internal class MusicRepository : IMusicRepository
{
    // TODO: Way too much going on here, need to split into smaller services

    private readonly IDataAccess _dataAccess;
    private readonly IJsonToModelConverter _jsonToModelConverter;
    private readonly IModelToJsonConverter _modelToJsonConverter;

    public MusicRepository(IDataAccess dataAccess, IJsonToModelConverter jsonToModelConverter, IModelToJsonConverter modelToJsonConverter)
    {
        _dataAccess = dataAccess;
        _jsonToModelConverter = jsonToModelConverter;
        _modelToJsonConverter = modelToJsonConverter;
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
            var artist = _jsonToModelConverter.ConvertArtist(jsonArtist);
            library.Artists.Add(artist);
        }
    }

    private async Task AddSource(FullLibrary library, List<JsonArtist> jsonArtists, LibrarySource source)
    {
        var jsonAlbumTrackLinks = await _dataAccess.GetAlbumTrackLinks(source);

        foreach (var jsonAlbumTrackLink in jsonAlbumTrackLinks)
        {
            var albumTrack = _jsonToModelConverter.ConvertAlbumTrackLink(jsonAlbumTrackLink);
            library.AlbumTrackLinks.Add(albumTrack);
        }

        var jsonTracks = await _dataAccess.GetTracks(source);

        foreach (var jsonTrack in jsonTracks)
        {
            var track = _jsonToModelConverter.ConvertTrack(jsonTrack, jsonArtists);
            track.Source = source;
            library.Tracks.Add(track);
        }

        var jsonAlbums = await _dataAccess.GetAlbums(source);

        foreach (var jsonAlbum in jsonAlbums)
        {
            var album = _jsonToModelConverter.ConvertAlbum(jsonAlbum, jsonArtists);
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

    public async Task RemoveTracks(LibrarySource source, List<string> trackIds)
    {
        var library = await _dataAccess.GetAll(source);

        foreach (var id in trackIds)
        {
            RemoveTrack(library, id);
        }

        await _dataAccess.SaveAll(library, source);
    }

    private static void RemoveTrack(JsonItems library, string trackId)
    {
        var track = library.Tracks.SingleOrDefault(a => a.Id == trackId);

        if (track == null)
            return;

        library.Tracks.Remove(track);

        var albumTrack = RemoveAlbumTrack(library, trackId);

        if (albumTrack != null)
        {
            library.AlbumTrackLinks.Remove(albumTrack);

            var removedAlbum = RemoveAlbumIfEmpty(library, albumTrack.AlbumId);

            if (removedAlbum != null)
            {
                RemoveArtistIfEmpty(library, removedAlbum.ArtistId);
            }
        }

        RemoveArtistIfEmpty(library, track.ArtistId);
    }

    private static JsonAlbumTrackLink RemoveAlbumTrack(JsonItems library, string trackId)
    {
        var albumTrack = library.AlbumTrackLinks.SingleOrDefault(t => t.TrackId == trackId);

        if (albumTrack == null)
            return null;

        library.AlbumTrackLinks.Remove(albumTrack);
        return albumTrack;
    }

    private static JsonAlbum RemoveAlbumIfEmpty(JsonItems library, string albumId)
    {
        var albumTracks = library.AlbumTrackLinks.Where(a => a.AlbumId == albumId);

        if (albumTracks.Any())
            return null;

        var album = library.Albums.SingleOrDefault(a => a.Id == albumId);
        if (album == null)
            return null;

        library.Albums.Remove(album);
        return album;
    }

    private static JsonArtist RemoveArtistIfEmpty(JsonItems library, string artistId)
    {
        var albums = library.Albums.Where(a => a.ArtistId == artistId);

        if (albums.Any())
            return null;

        var tracks = library.Tracks.Where(a => a.ArtistId == artistId);

        if (tracks.Any())
            return null;

        var artist = library.Artists.SingleOrDefault(a => a.Id == artistId);

        if (artist == null)
            return null;

        library.Artists.Remove(artist);
        return artist;
    }

    public async Task AddTrack(LibrarySource source, TrackFull track)
    {
        var library = await _dataAccess.GetAll(source);
        
        var existingTrack = library.Tracks.SingleOrDefault(a => a.Id == track.Track.Id);
        if (existingTrack == null)
        {
            var jsonTrack = _modelToJsonConverter.ConvertTrack(track.Track);
            library.Tracks.Add(jsonTrack);
        }

        var existingAlbum = library.Albums.SingleOrDefault(a => a.Id == track.Album.Id);
        if (existingAlbum == null)
        {
            var jsonAlbum = _modelToJsonConverter.ConvertAlbum(track.Album);
            library.Albums.Add(jsonAlbum);
        }

        var existingAlbumTrackLink = library.AlbumTrackLinks.SingleOrDefault(t => t.TrackId == track.Track.Id);
        if (existingAlbumTrackLink == null)
        {
            var jsonAlbumTrackLink = _modelToJsonConverter.ConvertAlbumTrackLink(track.AlbumTrack);
            library.AlbumTrackLinks.Add(jsonAlbumTrackLink);
        }

        var existingArtist = library.Artists.SingleOrDefault(a => a.Id == track.Artist.Id); 
        if (existingArtist == null)
        {
            var jsonArtist = _modelToJsonConverter.ConvertArtist(track.Artist);
            library.Artists.Add(jsonArtist);
        }

        if (track.Artist.Id != track.AlbumArtist.Id)
        {
            var existingAlbumArtist = library.Artists.SingleOrDefault(a => a.Id == track.AlbumArtist.Id);
            if (existingAlbumArtist == null)
            {
                var jsonAlbumArtist = _modelToJsonConverter.ConvertArtist(track.AlbumArtist);
                library.Artists.Add(jsonAlbumArtist);
            }
        }

        await _dataAccess.SaveAll(library, source);
    }
}
