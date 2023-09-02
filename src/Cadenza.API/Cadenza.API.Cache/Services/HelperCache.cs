﻿namespace Cadenza.API.Cache.Services;

internal class HelperCache : IHelperCache
{
    private readonly Dictionary<int, List<AlbumInfo>> _albumsByArtist = new();
    private readonly Dictionary<int, List<AlbumInfo>> _albumsFeaturingArtist = new();

    private readonly Dictionary<string, List<ArtistInfo>> _artistsByGenre = new();
    private readonly Dictionary<int, List<ArtistInfo>> _artistsByGrouping = new();

    private readonly Dictionary<int, List<(TrackInfo Track, AlbumTrackLink AlbumTrack)>> _tracksByAlbum = new();
    private readonly Dictionary<int, List<TrackInfo>> _tracksByArtist = new();

    public void CacheAlbum(AlbumInfo album)
    {
        _albumsByArtist.Cache(album.ArtistId, album);
    }

    public void CacheAlbumFeaturingArtist(int artistId, AlbumInfo album)
    {
        _albumsFeaturingArtist.Cache(artistId, album);
    }

    public void CacheAlbumTrack(AlbumTrackLink albumTrack, TrackInfo track)
    {
        _tracksByAlbum.Cache(albumTrack.AlbumId, (track, albumTrack));
    }

    public void CacheArtist(ArtistInfo artist)
    {
        _artistsByGrouping.Cache(artist.Grouping.Id, artist);
        _artistsByGenre.Cache(artist.Genre, artist);
    }

    public void CacheTrack(TrackInfo track)
    {
        _tracksByArtist.Cache(track.ArtistId, track);
    }

    public void Clear()
    {
        _albumsByArtist.Clear();
        _albumsFeaturingArtist.Clear();
        _artistsByGenre.Clear();
        _artistsByGrouping.Clear();
        _tracksByAlbum.Clear();
        _tracksByArtist.Clear();
    }

    public List<Album> GetAlbumsByArtist(int id)
    {
        return _albumsByArtist.GetList<int, AlbumInfo, Album>(id);
    }

    public List<Album> GetAlbumsFeaturingArtist(int id)
    {
        return _albumsFeaturingArtist.GetList<int, AlbumInfo, Album>(id);
    }

    public List<Artist> GetArtistsByGenre(string id)
    {
        return _artistsByGenre.GetList<string, ArtistInfo, Artist>(id);
    }

    public List<Artist> GetArtistsByGrouping(int id)
    {
        return _artistsByGrouping.GetList<int, ArtistInfo, Artist>(id);
    }

    public List<Track> GetArtistTracks(int id)
    {
        return _tracksByArtist.GetList<int, TrackInfo, Track>(id);
    }

    public List<AlbumTrack> GetAlbumTracks(int id)
    {
        var result = new List<AlbumTrack>();

        foreach (var track in _tracksByAlbum[id])
        {
            result.Add(new AlbumTrack(track.Track, track.AlbumTrack));
        }

        return result;
    }
}
