namespace Cadenza.API.Cache.Services;

internal class MainCache : IMainCache
{
    private readonly Dictionary<int, TrackDetailsDTO> _tracks = new();
    private readonly Dictionary<int, AlbumDetailsDTO> _albums = new();
    private readonly Dictionary<int, ArtistDetailsDTO> _artists = new();

    public void CacheAlbum(AlbumDetailsDTO album)
    {
        _albums.Cache(album.Id, album);
    }

    public void CacheArtist(ArtistDetailsDTO artist)
    {
        _artists.Cache(artist.Id, artist);

    }

    public void CacheTrack(TrackDetailsDTO track)
    {
        _tracks.Cache(track.Id, track);
    }

    public void Clear()
    {
        _albums.Clear();
        _artists.Clear();
        _tracks.Clear();
    }

    public AlbumDetailsDTO GetAlbum(int id)
    {
        return _albums.GetValue(id);
    }

    public ArtistDetailsDTO GetArtist(int id)
    {
        return _artists.GetValue(id);
    }

    public TrackDetailsDTO GetTrack(int id)
    {
        return _tracks.GetValue(id);
    }
}
