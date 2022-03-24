﻿namespace Cadenza.Library;

public class TrackCache : ITrackCache
{
    private Dictionary<string, TrackInfo> _tracks;
    private Dictionary<string, AlbumInfo> _albums;
    private Dictionary<string, ArtistInfo> _artists;
    private Dictionary<string, AlbumTrackLink> _albumTracks;

    public async Task<TrackFull> GetTrack(string id)
    {
        if (!_tracks.ContainsKey(id))
            return null;

        var track = _tracks[id];
        var album = _albums[track.AlbumId];
        var artist = _artists[track.ArtistId];
        var albumTrack = _albumTracks[track.Id];
        var albumArtist = _artists[album.ArtistId];

        return new TrackFull
        {
            Track = track,
            Artist = artist,
            Album = album,
            AlbumTrack = albumTrack,
            AlbumArtist = albumArtist
        };
    }

    public async Task Populate(FullLibrary library)
    {
        _tracks = library.Tracks.ToDictionary(a => a.Id, a => a);
        _albums = library.Albums.ToDictionary(a => a.Id, a => a);
        _artists = library.Artists.ToDictionary(a => a.Id, a => a);
        _albumTracks = library.AlbumTrackLinks.ToDictionary(a => a.TrackId, a => a);
    }

    public Task UpdateTrack(TrackUpdate update)
    {
        if (!_tracks.TryGetValue(update.Id, out TrackInfo track))
            return Task.CompletedTask;

        update.ApplyUpdates(track);

        return Task.CompletedTask;
    }
}
