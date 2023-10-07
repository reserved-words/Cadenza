namespace Cadenza.API.Cache.Services;

internal class MainCache : IMainCache
{
    private readonly Dictionary<int, TrackDetailsDTO> _tracks = new();
    private readonly Dictionary<int, AlbumDetailsDTO> _albums = new();
    private readonly Dictionary<int, ArtistDetailsDTO> _artists = new();
    private readonly Dictionary<int, AlbumTrackLinkDTO> _albumTracks = new();

    public void CacheAlbum(AlbumDetailsDTO album)
    {
        _albums.Cache(album.Id, album);
    }

    public void CacheAlbumTrack(AlbumTrackLinkDTO albumTrack)
    {
        _albumTracks.Cache(albumTrack.TrackId, albumTrack);
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
        _albumTracks.Clear();
        _artists.Clear();
        _tracks.Clear();
    }

    public AlbumDetailsDTO GetAlbum(int id)
    {
        return _albums.GetValue(id);
    }

    public AlbumTrackLinkDTO GetAlbumTrack(int trackId)
    {
        return _albumTracks.GetValue(trackId);
    }

    public List<ArtistDTO> GetAllArtists()
    {
        return _artists.GetAllValues<int, ArtistDetailsDTO, ArtistDTO>();
    }

    public ArtistDetailsDTO GetArtist(int id)
    {
        return _artists.GetValue(id);
    }

    public TrackFullDTO GetFullTrack(int id)
    {
        var track = GetTrack(id);
        var album = GetAlbum(track.AlbumId);

        return new TrackFullDTO
        {
            Track = track,
            Album = album,
            AlbumTrack = GetAlbumTrack(track.Id),
            Artist = GetArtist(track.ArtistId),
            AlbumArtist = GetArtist(album.ArtistId)
        };
    }

    public TrackDetailsDTO GetTrack(int id)
    {
        return _tracks.GetValue(id);
    }
}
