namespace Cadenza.API.Cache.Services;

internal class HelperCache : IHelperCache
{
    private readonly Dictionary<int, List<AlbumDetailsDTO>> _albumsByArtist = new();
    private readonly Dictionary<int, List<AlbumDetailsDTO>> _albumsFeaturingArtist = new();

    private readonly Dictionary<string, List<ArtistDetailsDTO>> _artistsByGenre = new();
    private readonly Dictionary<int, List<ArtistDetailsDTO>> _artistsByGrouping = new();

    private readonly Dictionary<int, AlbumTracksDTO> _tracksByAlbum = new();
    private readonly Dictionary<int, List<TrackDetailsDTO>> _tracksByArtist = new();

    public void CacheAlbum(AlbumDetailsDTO album)
    {
        _albumsByArtist.Cache(album.ArtistId, album);
    }

    public void CacheAlbumFeaturingArtist(int artistId, AlbumDetailsDTO album)
    {
        _albumsFeaturingArtist.Cache(artistId, album);
    }

    public void CacheAlbumTracks(AlbumTracksDTO albumTracks)
    {
        _tracksByAlbum.Cache(albumTracks.AlbumId, albumTracks);
    }

    public void CacheArtist(ArtistDetailsDTO artist)
    {
        _artistsByGrouping.Cache(artist.Grouping.Id, artist);
        _artistsByGenre.Cache(artist.Genre, artist);
    }

    public void CacheTrack(TrackDetailsDTO track)
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

    public List<AlbumDTO> GetAlbumsByArtist(int id)
    {
        return _albumsByArtist.GetList<int, AlbumDetailsDTO, AlbumDTO>(id);
    }

    public List<AlbumDTO> GetAlbumsFeaturingArtist(int id)
    {
        return _albumsFeaturingArtist.GetList<int, AlbumDetailsDTO, AlbumDTO>(id);
    }

    public List<ArtistDTO> GetArtistsByGenre(string id)
    {
        return _artistsByGenre.GetList<string, ArtistDetailsDTO, ArtistDTO>(id);
    }

    public List<ArtistDTO> GetArtistsByGrouping(int id)
    {
        return _artistsByGrouping.GetList<int, ArtistDetailsDTO, ArtistDTO>(id);
    }

    public List<TrackDTO> GetArtistTracks(int id)
    {
        return _tracksByArtist.GetList<int, TrackDetailsDTO, TrackDTO>(id);
    }

    public AlbumTracksDTO GetAlbumTracks(int id)
    {
        return _tracksByAlbum.TryGetValue(id, out var albumTracks)
            ? albumTracks
            : new AlbumTracksDTO { AlbumId = id, Discs = new List<AlbumDiscDTO>() };
    }
}
