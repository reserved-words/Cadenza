using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Services;

internal class MainCache : IMainCache
{
    private readonly Dictionary<int, TrackDetails> _tracks = new();
    private readonly Dictionary<int, AlbumDetails> _albums = new();
    private readonly Dictionary<int, ArtistDetails> _artists = new();
    private readonly Dictionary<int, AlbumTrackLink> _albumTracks = new();

    public void CacheAlbum(AlbumDetails album)
    {
        _albums.Cache(album.Id, album);
    }

    public void CacheAlbumTrack(AlbumTrackLink albumTrack)
    {
        _albumTracks.Cache(albumTrack.TrackId, albumTrack);
    }

    public void CacheArtist(ArtistDetails artist)
    {
        _artists.Cache(artist.Id, artist);

    }

    public void CacheTrack(TrackDetails track)
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

    public AlbumDetails GetAlbum(int id)
    {
        return _albums.GetValue(id);
    }

    public AlbumTrackLink GetAlbumTrack(int trackId)
    {
        return _albumTracks.GetValue(trackId);
    }

    public List<Artist> GetAllArtists()
    {
        return _artists.GetAllValues<int, ArtistDetails, Artist>();
    }

    public ArtistDetails GetArtist(int id)
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

    public TrackDetails GetTrack(int id)
    {
        return _tracks.GetValue(id);
    }
}
