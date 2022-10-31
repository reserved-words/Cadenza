namespace Cadenza.API.Core.Services.Cache;

internal class PlayTrackCache : IPlayTrackCache
{
    private List<PlayTrack> _tracks = new();
    private Dictionary<string, List<PlayTrack>> _albumTracks = new();
    private Dictionary<string, List<PlayTrack>> _artistTracks = new();
    private Dictionary<Grouping, List<string>> _groupingArtists = new();
    private Dictionary<string, List<string>> _genreArtists = new();

    private Dictionary<string, List<string>> _tagArtists = new();
    private Dictionary<string, List<string>> _tagAlbums = new();
    private Dictionary<string, List<PlayTrack>> _tagTracks = new();

    public Task<List<PlayTrack>> GetAll()
    {
        var result = _tracks.ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> GetByAlbum(string id)
    {
        var result = _albumTracks.TryGetValue(id, out List<PlayTrack> tracks)
            ? tracks
            : new List<PlayTrack>();

        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> GetByArtist(string id)
    {
        var result = _artistTracks.TryGetValue(id, out List<PlayTrack> tracks)
            ? tracks
            : new List<PlayTrack>();

        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> GetByGenre(string id)
    {
        var genreArtists = _genreArtists.TryGetValue(id, out List<string> artistIds)
            ? artistIds
            : new List<string>();

        var result = genreArtists
            .SelectMany(a => _artistTracks[a])
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> GetByGrouping(Grouping id)
    {
        var groupingArtists = _groupingArtists.TryGetValue(id, out List<string> artistIds)
            ? artistIds
            : new List<string>();

        var result = groupingArtists
            .SelectMany(a => _artistTracks[a])
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<PlayTrack>> GetByTag(string id)
    {
        var result = _tagTracks.TryGetValue(id, out List<PlayTrack> tracks)
            ? tracks
            : new List<PlayTrack>();

        if (_tagAlbums.TryGetValue(id, out var albums))
        {
            result.AddRange(albums.SelectMany(a => _albumTracks[a]));
        }

        if (_tagArtists.TryGetValue(id, out var artists))
        {
            result.AddRange(artists.SelectMany(a => _artistTracks[a]));
        }

        return Task.FromResult(result);
    }

    public Task Populate(FullLibrary library)
    {
        foreach (var artist in library.Artists)
        {
            _artistTracks.Add(artist.Id, new List<PlayTrack>());

            AddArtistToGrouping(artist);
            AddArtistToGenre(artist);

            foreach (var tag in artist.Tags.Tags)
            {
                AddArtistToTag(artist, tag);
            }
        }

        foreach (var album in library.Albums)
        {
            _albumTracks.Add(album.Id, new List<PlayTrack>());

            foreach (var tag in album.Tags.Tags)
            {
                AddAlbumToTag(album, tag);
            }
        }

        var trackDictionary = new Dictionary<string, PlayTrack>();

        foreach (var track in library.Tracks)
        {
            var playTrack = new PlayTrack
            {
                Id = track.Id,
                Title = track.Title,
                ArtistId = track.ArtistId,
                AlbumId = track.AlbumId,
                Source = track.Source
            };

            _tracks.Add(playTrack);

            trackDictionary.Add(track.Id, playTrack);
            _artistTracks[track.ArtistId].Add(playTrack);

            foreach (var tag in track.Tags.Tags)
            {
                AddTrackToTag(track, tag, playTrack);
            }
        }

        var albums = library.AlbumTracks
            .GroupBy(a => a.AlbumId);

        foreach (var album in albums)
        {
            _albumTracks[album.Key] = album
                .OrderBy(t => t.DiscNo)
                .ThenBy(t => t.TrackNo)
                .Select(t => trackDictionary[t.TrackId])
                .ToList();
        }

        return Task.CompletedTask;
    }

    private void AddArtistToGenre(ArtistInfo artist)
    {
        var genre = artist.Genre ?? "None";

        if (!_genreArtists.TryGetValue(genre, out List<string> genreArtists))
        {
            genreArtists = new List<string>();
            _genreArtists.Add(genre, genreArtists);
        }

        genreArtists.Add(artist.Id);
    }

    private void AddArtistToGrouping(ArtistInfo artist)
    {
        if (!_groupingArtists.TryGetValue(artist.Grouping, out List<string> groupingArtists))
        {
            groupingArtists = new List<string>();
            _groupingArtists.Add(artist.Grouping, groupingArtists);
        }

        groupingArtists.Add(artist.Id);
    }

    private void AddArtistToTag(ArtistInfo artist, string tag)
    {
        if (!_tagArtists.TryGetValue(tag, out List<string> tagArtists))
        {
            tagArtists = new List<string>();
            _tagArtists.Add(tag, tagArtists);
        }

        tagArtists.Add(artist.Id);
    }

    private void AddAlbumToTag(AlbumInfo album, string tag)
    {
        if (_tagArtists.TryGetValue(tag, out List<string> tagArtists) && tagArtists.Contains(album.ArtistId))
            return;

        if (!_tagAlbums.TryGetValue(tag, out List<string> tagAlbums))
        {
            tagAlbums = new List<string>();
            _tagAlbums.Add(tag, tagAlbums);
        }

        tagAlbums.Add(album.Id);
    }

    private void AddTrackToTag(TrackInfo track, string tag, PlayTrack playTrack)
    {
        if (_tagArtists.TryGetValue(tag, out List<string> tagArtists) && tagArtists.Contains(track.ArtistId))
            return;

        if (_tagAlbums.TryGetValue(tag, out List<string> tagAlbums) && tagAlbums.Contains(track.AlbumId))
            return;

        if (!_tagTracks.TryGetValue(tag, out List<PlayTrack> tagTracks))
        {
            tagTracks = new List<PlayTrack>();
            _tagTracks.Add(tag, tagTracks);
        }

        tagTracks.Add(playTrack);
    }
}
