using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.Cache.Services;

internal class HelperCache : IHelperCache
{
    private readonly Dictionary<int, List<AlbumDetails>> _albumsByArtist = new();
    private readonly Dictionary<int, List<AlbumDetails>> _albumsFeaturingArtist = new();

    private readonly Dictionary<string, List<ArtistDetails>> _artistsByGenre = new();
    private readonly Dictionary<int, List<ArtistDetails>> _artistsByGrouping = new();

    private readonly Dictionary<int, List<(TrackDetails Track, AlbumTrackLink AlbumTrack)>> _tracksByAlbum = new();
    private readonly Dictionary<int, List<TrackDetails>> _tracksByArtist = new();

    public void CacheAlbum(AlbumDetails album)
    {
        _albumsByArtist.Cache(album.ArtistId, album);
    }

    public void CacheAlbumFeaturingArtist(int artistId, AlbumDetails album)
    {
        _albumsFeaturingArtist.Cache(artistId, album);
    }

    public void CacheAlbumTrack(AlbumTrackLink albumTrack, TrackDetails track)
    {
        _tracksByAlbum.Cache(albumTrack.AlbumId, (track, albumTrack));
    }

    public void CacheArtist(ArtistDetails artist)
    {
        _artistsByGrouping.Cache(artist.Grouping.Id, artist);
        _artistsByGenre.Cache(artist.Genre, artist);
    }

    public void CacheTrack(TrackDetails track)
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
        return _albumsByArtist.GetList<int, AlbumDetails, Album>(id);
    }

    public List<Album> GetAlbumsFeaturingArtist(int id)
    {
        return _albumsFeaturingArtist.GetList<int, AlbumDetails, Album>(id);
    }

    public List<Artist> GetArtistsByGenre(string id)
    {
        return _artistsByGenre.GetList<string, ArtistDetails, Artist>(id);
    }

    public List<Artist> GetArtistsByGrouping(int id)
    {
        return _artistsByGrouping.GetList<int, ArtistDetails, Artist>(id);
    }

    public List<Track> GetArtistTracks(int id)
    {
        return _tracksByArtist.GetList<int, TrackDetails, Track>(id);
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
