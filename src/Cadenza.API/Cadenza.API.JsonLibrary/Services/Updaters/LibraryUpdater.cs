using Cadenza.API.JsonLibrary.Interfaces.Updaters;

namespace Cadenza.API.JsonLibrary.Services.Updaters;

internal class LibraryUpdater : ILibraryUpdater
{
    public void AddTrack(FullLibrary library, TrackFull track)
    {
        var existingTrack = library.Tracks.SingleOrDefault(a => a.Id == track.Track.Id);
        if (existingTrack == null)
        {
            library.Tracks.Add(track.Track);
        }

        var existingAlbum = library.Albums.SingleOrDefault(a => a.Id == track.Album.Id);
        if (existingAlbum == null)
        {
            library.Albums.Add(track.Album);
        }

        var existingAlbumTrackLink = library.AlbumTracks.SingleOrDefault(t => t.TrackId == track.Track.Id);
        if (existingAlbumTrackLink == null)
        {
            library.AlbumTracks.Add(track.AlbumTrack);
        }

        var existingArtist = library.Artists.SingleOrDefault(a => a.Id == track.Artist.Id);
        if (existingArtist == null)
        {
            library.Artists.Add(track.Artist);
        }

        if (track.Artist.Id != track.AlbumArtist.Id)
        {
            var existingAlbumArtist = library.Artists.SingleOrDefault(a => a.Id == track.AlbumArtist.Id);
            if (existingAlbumArtist == null)
            {
                library.Artists.Add(track.AlbumArtist);
            }
        }
    }

    public void RemoveTracks(FullLibrary library, List<string> trackIds)
    {
        foreach (var id in trackIds)
        {
            RemoveTrack(library, id);
        }
    }

    private void RemoveTrack(FullLibrary library, string trackId)
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

    private static AlbumTrackLink RemoveAlbumTrack(FullLibrary library, string trackId)
    {
        var albumTrack = library.AlbumTracks.SingleOrDefault(t => t.TrackId == trackId);

        if (albumTrack == null)
            return null;

        library.AlbumTracks.Remove(albumTrack);
        return albumTrack;
    }

    private static AlbumInfo RemoveAlbumIfEmpty(FullLibrary library, string albumId)
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

    private static ArtistInfo RemoveArtistIfEmpty(FullLibrary library, string artistId)
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
