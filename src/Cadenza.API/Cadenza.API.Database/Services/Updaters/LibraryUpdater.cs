using Cadenza.API.Database.Interfaces.Updaters;

namespace Cadenza.API.Database.Services.Updaters;

internal class LibraryUpdater : ILibraryUpdater
{
    private readonly IModelToJsonConverter _modelConverter;

    public LibraryUpdater(IModelToJsonConverter modelToJsonConverter)
    {
        _modelConverter = modelToJsonConverter;
    }

    public void AddTrack(JsonItems library, TrackFull track)
    {
        var existingTrack = library.Tracks.SingleOrDefault(a => a.Id == track.Track.Id);
        if (existingTrack == null)
        {
            var jsonTrack = _modelConverter.ConvertTrack(track.Track);
            library.Tracks.Add(jsonTrack);
        }

        var existingAlbum = library.Albums.SingleOrDefault(a => a.Id == track.Album.Id);
        if (existingAlbum == null)
        {
            var jsonAlbum = _modelConverter.ConvertAlbum(track.Album);
            library.Albums.Add(jsonAlbum);
        }

        var existingAlbumTrackLink = library.AlbumTracks.SingleOrDefault(t => t.TrackId == track.Track.Id);
        if (existingAlbumTrackLink == null)
        {
            var jsonAlbumTrackLink = _modelConverter.ConvertAlbumTrackLink(track.AlbumTrack);
            library.AlbumTracks.Add(jsonAlbumTrackLink);
        }

        var existingArtist = library.Artists.SingleOrDefault(a => a.Id == track.Artist.Id);
        if (existingArtist == null)
        {
            var jsonArtist = _modelConverter.ConvertArtist(track.Artist);
            library.Artists.Add(jsonArtist);
        }

        if (track.Artist.Id != track.AlbumArtist.Id)
        {
            var existingAlbumArtist = library.Artists.SingleOrDefault(a => a.Id == track.AlbumArtist.Id);
            if (existingAlbumArtist == null)
            {
                var jsonAlbumArtist = _modelConverter.ConvertArtist(track.AlbumArtist);
                library.Artists.Add(jsonAlbumArtist);
            }
        }
    }

    public void RemoveTracks(JsonItems library, List<string> trackIds)
    {
        foreach (var id in trackIds)
        {
            RemoveTrack(library, id);
        }
    }

    private void RemoveTrack(JsonItems library, string trackId)
    {
        var track = library.Tracks.SingleOrDefault(a => a.Id == trackId);

        if (track == null)
            return;

        library.Tracks.Remove(track);

        var albumTrack = RemoveAlbumTrack(library, trackId);

        if (albumTrack != null)
        {
            library.AlbumTracks.Remove(albumTrack);

            var removedAlbum = RemoveAlbumIfEmpty(library, albumTrack.AlbumId);

            if (removedAlbum != null)
            {
                RemoveArtistIfEmpty(library, removedAlbum.ArtistId);
            }
        }

        RemoveArtistIfEmpty(library, track.ArtistId);
    }

    private static JsonAlbumTrack RemoveAlbumTrack(JsonItems library, string trackId)
    {
        var albumTrack = library.AlbumTracks.SingleOrDefault(t => t.TrackId == trackId);

        if (albumTrack == null)
            return null;

        library.AlbumTracks.Remove(albumTrack);
        return albumTrack;
    }

    private static JsonAlbum RemoveAlbumIfEmpty(JsonItems library, string albumId)
    {
        var albumTracks = library.AlbumTracks.Where(a => a.AlbumId == albumId);

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
}
