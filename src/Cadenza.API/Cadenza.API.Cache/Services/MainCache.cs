﻿using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Services;

internal class MainCache : IMainCache
{
    private readonly Dictionary<int, TrackInfo> _tracks = new();
    private readonly Dictionary<int, AlbumInfo> _albums = new();
    private readonly Dictionary<int, ArtistInfo> _artists = new();
    private readonly Dictionary<int, AlbumTrackLink> _albumTracks = new();

    public void CacheAlbum(AlbumInfo album)
    {
        _albums.Cache(album.Id, album);
    }

    public void CacheAlbumTrack(AlbumTrackLink albumTrack)
    {
        _albumTracks.Cache(albumTrack.TrackId, albumTrack);
    }

    public void CacheArtist(ArtistInfo artist)
    {
        _artists.Cache(artist.Id, artist);

    }

    public void CacheTrack(TrackInfo track)
    {
        _tracks.Cache(track.Id, track);
    }

    public void Clear()
    {
        _albums.Clear();
        _albumTracks.Clear();
        _artists.Clear();
        _tracks.Clear();
    }

    public AlbumInfo GetAlbum(int id)
    {
        return _albums.GetValue(id);
    }

    public AlbumTrackLink GetAlbumTrack(int trackId)
    {
        return _albumTracks.GetValue(trackId);
    }

    public List<Artist> GetAllArtists()
    {
        return _artists.GetAllValues<int, ArtistInfo, Artist>();
    }

    public ArtistInfo GetArtist(int id)
    {
        return _artists.GetValue(id);
    }

    public TrackFull GetFullTrack(int id)
    {
        var track = GetTrack(id);
        var album = GetAlbum(track.AlbumId);

        return new TrackFull
        {
            Track = track,
            Album = album,
            AlbumTrack = GetAlbumTrack(track.Id),
            Artist = GetArtist(track.ArtistId),
            AlbumArtist = GetArtist(album.ArtistId)
        };
    }

    public TrackInfo GetTrack(int id)
    {
        return _tracks.GetValue(id);
    }
}
