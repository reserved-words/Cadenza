using Cadenza.API.Common.Interfaces.Cache;
using Cadenza.Domain;

namespace Cadenza.API.Database.Services.Cache;

public class PlayTrackCache : IPlayTrackCache
{
    private List<PlayTrack> _tracks;
    private Dictionary<string, List<PlayTrack>> _albumTracks;
    private Dictionary<string, List<PlayTrack>> _artistTracks;
    private Dictionary<Grouping, List<string>> _groupingArtists;
    private Dictionary<string, List<string>> _genreArtists;

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

    public Task Populate(FullLibrary library)
    {
        _groupingArtists = library.Artists
            .GroupBy(a => a.Grouping)
            .ToDictionary(g => g.Key, g => g.Select(a => a.Id).ToList());

        _genreArtists = library.Artists
            .GroupBy(a => a.Genre ?? "None")
            .ToDictionary(g => g.Key, g => g.Select(a => a.Id).ToList());

        _tracks = library.Tracks
            .Select(t => new PlayTrack
            {
                Id = t.Id,
                Title = t.Title,
                ArtistId = t.ArtistId,
                AlbumId = t.AlbumId,
                Source = t.Source
            })
            .ToList();

        _artistTracks = library.Artists.ToDictionary(a => a.Id, a => new List<PlayTrack>());
        _albumTracks = library.Albums.ToDictionary(a => a.Id, a => new List<PlayTrack>());

        var trackDictionary = new Dictionary<string, PlayTrack>();

        foreach (var track in _tracks)
        {
            trackDictionary.Add(track.Id, track);
            _artistTracks[track.ArtistId].Add(track);
        }

        var albums = library.AlbumTrackLinks
            .GroupBy(a => a.AlbumId);

        foreach (var album in albums)
        {
            _albumTracks[album.Key] = album
                .OrderBy(t => t.Position.DiscNo)
                .ThenBy(t => t.Position.TrackNo)
                .Select(t => trackDictionary[t.TrackId])
                .ToList();
        }

        return Task.CompletedTask;
    }
}
