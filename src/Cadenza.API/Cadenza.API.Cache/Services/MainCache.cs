namespace Cadenza.API.Cache.Services;

internal class MainCache : IMainCache
{
    private readonly Dictionary<string, TrackInfo> _tracks = new();
    private readonly Dictionary<int, AlbumInfo> _albums = new();
    private readonly Dictionary<string, ArtistInfo> _artists = new();
    private readonly Dictionary<string, AlbumTrackLink> _albumTracks = new();

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

    public AlbumTrackLink GetAlbumTrack(string trackId)
    {
        return _albumTracks.GetValue(trackId);
    }

    public List<Artist> GetAllArtists()
    {
        return _artists.GetAllValues<string, ArtistInfo, Artist>();
    }

    public ArtistInfo GetArtist(string id)
    {
        return _artists.GetValue(id);
    }

    public TrackFull GetFullTrack(string id)
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

    public TrackInfo GetTrack(string id)
    {
        return _tracks.GetValue(id);
    }
}
