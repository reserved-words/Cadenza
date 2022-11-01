﻿namespace Cadenza.API.Core.Services.Cache;

internal class Cache : ICache
{
    private Dictionary<string, PlayTrack> _playTracks = new();
    private Dictionary<string, List<PlayTrack>> _tagPlayTracks = new();

    private readonly Dictionary<PlayerItemType, List<PlayerItem>> _items = new();
    private readonly Dictionary<string, List<PlayerItem>> _tags = new();

    private Dictionary<string, TrackInfo> _tracks = new();
    private Dictionary<string, AlbumInfo> _albums = new();
    private Dictionary<string, ArtistInfo> _artists = new();
    private Dictionary<string, AlbumTrackLink> _albumTracks = new();

    private Dictionary<Grouping, List<ArtistInfo>> _artistsByGrouping = new();
    private Dictionary<string, List<ArtistInfo>> _artistsByGenre = new();

    private Dictionary<string, List<AlbumInfo>> _albumsByArtist = new();
    private Dictionary<string, List<TrackInfo>> _tracksByArtist = new();
    private Dictionary<string, List<TrackInfo>> _tracksByAlbum = new();

    public Task Populate(FullLibrary library)
    {
        foreach (var artist in library.Artists)
        {
            if (string.IsNullOrWhiteSpace(artist.Genre))
            {
                artist.Genre = "None";
            }

            _artists.Cache(artist.Id, artist);
            _artistsByGrouping.Cache(artist.Grouping, artist);
            _artistsByGenre.Cache(artist.Genre, artist);

            var item = new SearchableArtist(artist);
            _items.Cache(PlayerItemType.Artist, item);
            _tags.Cache(artist.Tags, item);
            _items.Cache(PlayerItemType.Grouping, artist.Grouping.ToString(), () => new SearchableGrouping(artist.Grouping));
            _items.Cache(PlayerItemType.Genre, artist.Genre, () => new SearchableGenre(artist.Genre));

        }

        foreach (var album in library.Albums)
        {
            _albums.Cache(album.Id, album);
            _albumsByArtist.Cache(album.ArtistId, album);

            var item = new SearchableAlbum(album);
            _items.Cache(PlayerItemType.Album, item);
            _tags.Cache(album.Tags, item);
        }

        foreach (var track in library.Tracks)
        {
            _tracks.Cache(track.Id, track);
            _tracksByArtist.Cache(track.ArtistId, track);

            var item = new SearchableTrack(track, _albums[track.AlbumId]);
            _items.Cache(PlayerItemType.Track, item);
            _tags.Cache(track.Tags, item);

            var artist = _artists[track.ArtistId];
            var album = _albums[track.AlbumId];

            var playTrack = new PlayTrack
            {
                Id = track.Id,
                Title = track.Title,
                ArtistId = track.ArtistId,
                AlbumId = track.AlbumId,
                Source = track.Source
            };

            _playTracks.Add(playTrack.Id, playTrack);

            _tagPlayTracks.Cache(track, artist, album, playTrack);
        }

        var albumTracks = library.AlbumTracks
            .OrderBy(at => at.AlbumId)
            .ThenBy(at => at.DiscNo)
            .ThenBy(at => at.TrackNo)
            .ToList();

        foreach (var albumTrack in albumTracks)
        {
            _albumTracks.Cache(albumTrack.TrackId, albumTrack);
            _tracksByAlbum.Cache(albumTrack.AlbumId, _tracks[albumTrack.TrackId]);
        }

        return Task.CompletedTask;
    }

    public Task<AlbumInfo> GetAlbum(string id)
    {
        var result = _albums.TryGetValue(id, out AlbumInfo album)
            ? album
            : null;

        return Task.FromResult(result);
    }

    public Task<List<AlbumTrack>> GetAlbumTracks(string id)
    {
        var result = new List<AlbumTrack>();

        foreach (var track in _tracksByAlbum[id])
        {
            var albumTrack = _albumTracks[track.Id];

            result.Add(new AlbumTrack
            {
                TrackId = track.Id,
                Title = track.Title,
                ArtistId = track.ArtistId,
                ArtistName = track.ArtistName,
                DurationSeconds = track.DurationSeconds,
                DiscNo = albumTrack.DiscNo,
                TrackNo = albumTrack.TrackNo
            });
        }

        return Task.FromResult(result);
    }

    public async Task<List<Artist>> GetAlbumArtists()
    {
        return _albumsByArtist.Keys
            .Select(a => _artists[a])
            .OfType<Artist>()
            .ToList();
    }

    public Task<List<Album>> GetAlbums(string artistId)
    {
        var result = _albumsByArtist.TryGetValue(artistId, out List<AlbumInfo> albums)
            ? albums.OfType<Album>().ToList()
            : new List<Album>();

        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetAllArtists()
    {
        var result = _artists
            .Values.OfType<Artist>().ToList()
            .ToList();

        return Task.FromResult(result);
    }

    public Task<ArtistInfo> GetArtist(string id)
    {
        var result = _artists.TryGetValue(id, out ArtistInfo artist)
            ? artist
            : null;

        return Task.FromResult(result);
    }

    public Task<List<Artist>> GetArtistsByGenre(string id)
    {
        var artists = _artistsByGenre.TryGetValue(id, out List<ArtistInfo> result)
            ? result.OfType<Artist>().ToList()
            : new List<Artist>();

        return Task.FromResult(artists);
    }

    public Task<List<Artist>> GetArtistsByGrouping(Grouping id)
    {
        var artists = _artistsByGrouping.TryGetValue(id, out List<ArtistInfo> result)
            ? result.OfType<Artist>().ToList()
            : new List<Artist>();

        return Task.FromResult(artists);
    }

    public Task<List<Artist>> GetTrackArtists()
    {
        var result = _tracksByArtist.Keys
            .Select(a => _artists[a])
            .OfType<Artist>()
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<Track>> GetArtistTracks(string artistId)
    {
        var result = _tracksByArtist.TryGetValue(artistId, out List<TrackInfo> tracks)
            ? tracks.OfType<Track>().ToList()
            : new List<Track>();

        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchAlbums()
    {
        return Task.FromResult(_items[PlayerItemType.Album]);
    }

    public Task<List<PlayerItem>> GetSearchArtists()
    {
        return Task.FromResult(_items[PlayerItemType.Artist]);
    }

    public Task<List<PlayerItem>> GetSearchTags()
    {
        var result = _tags.Keys.Select(t => new SearchableTag(t)).OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchTracks()
    {
        return Task.FromResult(_items[PlayerItemType.Track]);
    }

    public Task<List<PlayerItem>> GetSearchGenres()
    {
        return Task.FromResult(_items[PlayerItemType.Genre]);
    }

    public Task<List<PlayerItem>> GetSearchGroupings()
    {
        return Task.FromResult(_items[PlayerItemType.Grouping]);
    }

    public Task<List<PlayerItem>> GetTag(string id)
    {
        return Task.FromResult(_tags[id]);
    }
    public Task<TrackFull> GetTrack(string id)
    {
        if (!_tracks.ContainsKey(id))
            return null;

        var track = _tracks[id];
        var album = _albums[track.AlbumId];

        var result = new TrackFull
        {
            Track = track,
            Album = album,
            AlbumTrack = _albumTracks[track.Id],
            Artist = _artists[track.ArtistId],
            AlbumArtist = _artists[album.ArtistId]
        };

        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayAll()
    {
        return Task.FromResult(_playTracks.Values.ToList());
    }

    public Task<List<PlayTrack>> PlayAlbum(string id)
    {
        var result = _tracksByAlbum[id].Select(t => _playTracks[t.Id]).ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> PlayArtist(string id)
    {
        var result = _tracksByArtist[id].Select(t => _playTracks[t.Id]).ToList();
        return Task.FromResult(result);
    }

    public async Task<List<PlayTrack>> PlayGenre(string id)
    {
        var result = new List<PlayTrack>();

        foreach (var artist in _artistsByGenre[id])
        {
            result.AddRange(await PlayArtist(artist.Id));
        }

        return result;
    }

    public async Task<List<PlayTrack>> PlayGrouping(Grouping id)
    {
        var result = new List<PlayTrack>();

        foreach (var artist in _artistsByGrouping[id])
        {
            result.AddRange(await PlayArtist(artist.Id));
        }

        return result;
    }

    public Task<List<PlayTrack>> PlayTag(string id)
    {
        return Task.FromResult(_tagPlayTracks[id]);
    }

    public Task UpdateTrack(TrackUpdate update)
    {
        if (!_tracks.TryGetValue(update.Id, out TrackInfo track))
            return Task.CompletedTask;

        update.ApplyUpdates(track);

        return Task.CompletedTask;
    }

    public Task UpdateAlbum(AlbumUpdate update)
    {
        if (!_albums.TryGetValue(update.Id, out AlbumInfo album))
            return Task.CompletedTask;

        update.ApplyUpdates(album);

        return Task.CompletedTask;
    }

    public Task UpdateArtist(ArtistUpdate update)
    {
        if (!_artists.TryGetValue(update.Id, out ArtistInfo artist))
            return Task.CompletedTask;

        update.ApplyUpdates(artist);

        if (update.IsUpdated(ItemProperty.Genre, out PropertyUpdate genreUpdate))
        {
            var originalGenreArtists = _artistsByGenre.GetOrAdd(genreUpdate.OriginalValue);
            var updatedGenreArtists = _artistsByGenre.GetOrAdd(genreUpdate.UpdatedValue);

            originalGenreArtists.RemoveWhere(a => a.Id == update.Id);
            updatedGenreArtists.RemoveWhere(a => a.Id == update.Id);
            updatedGenreArtists.AddThenSort(update.UpdatedItem, a => a.Genre);
        }

        if (update.IsUpdated(ItemProperty.Grouping, out PropertyUpdate groupingUpdate))
        {
            var originalGrouping = groupingUpdate.OriginalValue.Parse<Grouping>();
            var updatedGrouping = groupingUpdate.UpdatedValue.Parse<Grouping>();

            var originalGroupingArtists = _artistsByGrouping.GetOrAdd(originalGrouping);
            var updatedGroupingArtists = _artistsByGrouping.GetOrAdd(updatedGrouping);

            originalGroupingArtists.RemoveWhere(a => a.Id == update.Id);
            updatedGroupingArtists.RemoveWhere(a => a.Id == update.Id);
            updatedGroupingArtists.AddThenSort(update.UpdatedItem, a => a.Genre);
        }

        return Task.CompletedTask;
    }
}