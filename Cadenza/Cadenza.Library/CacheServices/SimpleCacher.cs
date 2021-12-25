namespace Cadenza.Library;

internal class SimpleCacher : ICacher
{
    private readonly ICache _cache;
    private readonly IMerger _merger;

    public SimpleCacher(IMerger merger, ICache cache)
    {
        _merger = merger;
        _cache = cache;
    }

    public void AddArtist(ArtistInfo artist, bool asAlbumArtist, bool forceUpdate)
    {
        var existingArtist = _cache.Artists.GetOrAdd(artist.Id);
        _merger.MergeArtistInfo(existingArtist, artist, forceUpdate);

        if (asAlbumArtist)
        {
            _cache.AlbumArtists.AddIfNotPresent(artist.Id);
        }
    }

    public void AddAlbum(AlbumInfo album, bool forceUpdate)
    {
        var existingAlbum = _cache.Albums.GetOrAdd(album.Id);
        _merger.MergeAlbum(existingAlbum, album, forceUpdate);

        var albumLinks = _cache.AlbumLinks.GetOrAdd(album.Id);
        albumLinks.AlbumId ??= album.Id;
        _merger.MergeAlbumArtist(albumLinks, album.ArtistId);

        if (album.ArtistId != null)
        {
            var artistLinks = _cache.ArtistLinks.GetOrAdd(album.ArtistId);
            artistLinks.ArtistId ??= album.ArtistId;
            _merger.MergeArtistAlbum(artistLinks, album.Id);

            _cache.AlbumArtists.AddIfNotPresent(album.ArtistId);
        }
    }

    public void AddTrack(TrackInfo track, bool forceUpdate)
    {
        var artistId = track.ArtistId;

        var existingTrack = _cache.Tracks.GetOrAdd(track.Id);
        _merger.MergeTrack(existingTrack, track, forceUpdate);

        var trackLinks = _cache.TrackLinks.GetOrAdd(track.Id);
        trackLinks.TrackId ??= track.Id;
        _merger.MergeTrackArtist(trackLinks, artistId);

        var artistLinks = _cache.ArtistLinks.GetOrAdd(track.ArtistId);
        artistLinks.ArtistId ??= track.ArtistId;
        _merger.MergeArtistTrack(artistLinks, track.Id);
    }

    public void AddAlbumTrack(AlbumTrackLink albumTrack)
    {
        var albumLinks = _cache.AlbumLinks.GetOrAdd(albumTrack.AlbumId);
        albumLinks.AlbumId ??= albumTrack.AlbumId;
        _merger.MergeAlbumTrack(albumLinks, albumTrack.TrackId, albumTrack.Position);

        var trackLinks = _cache.TrackLinks.GetOrAdd(albumTrack.TrackId);
        trackLinks.TrackId ??= albumTrack.TrackId;
        _merger.MergeTrackAlbum(trackLinks, albumTrack.AlbumId, albumTrack.Position);
    }
}