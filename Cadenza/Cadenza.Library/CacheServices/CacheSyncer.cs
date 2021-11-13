namespace Cadenza.Library;

internal class CacheSyncer : IComplexCacher
{
    private readonly ISimpleCacher _itemCacher;

    public CacheSyncer(ISimpleCacher cache)
    {
        _itemCacher = cache;
    }

    public void AddArtist(ArtistFull artist)
    {
        _itemCacher.AddArtist(artist.Artist, false, false);

        foreach (var album in artist.Albums)
        {
            _itemCacher.AddAlbum(album.Album, false);
            foreach (var track in album.AlbumTracks)
            {
                _itemCacher.AddTrack(GetTrackInfo(track.Track), false);

                _itemCacher.AddAlbumTrack(new AlbumTrackLink
                {
                    AlbumId = album.Album.Id,
                    TrackId = track.Track.Id,
                    Position = track.Position
                });
            }
        }
    }

    public void AddAlbumArtists(ICollection<Artist> artists)
    {
        foreach (var artist in artists)
        {
            _itemCacher.AddArtist(GetArtistInfo(artist), true, false);
        }
    }

    public void AddTrack(TrackFull track)
    {
        _itemCacher.AddTrack(track.Track, false);
        _itemCacher.AddAlbum(track.Album, false);
        _itemCacher.AddArtist(track.Artist, false, false);
        _itemCacher.AddAlbumTrack(new AlbumTrackLink
        {
            AlbumId = track.Album.Id,
            TrackId = track.Track.Id,
            Position = track.Position
        });
    }

    public void AddTracks(ICollection<Track> tracks)
    {
        foreach (var track in tracks)
        {
            _itemCacher.AddTrack(GetTrackInfo(track), false);
        }
    }

    private TrackInfo GetTrackInfo(Track track)
    {
        return new TrackInfo
        {
            Id = track.Id,
            ArtistId = track.ArtistId,
            Title = track.Title,
            ArtistName = track.ArtistName,
            DurationSeconds = track.DurationSeconds,
            Source = track.Source
        };
    }

    private ArtistInfo GetArtistInfo(Artist artist)
    {
        return new ArtistInfo
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping,
            Genre = artist.Genre,
            SourceIds = artist.SourceIds
        };
    }
}