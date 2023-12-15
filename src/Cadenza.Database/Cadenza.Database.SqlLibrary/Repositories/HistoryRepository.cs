using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class HistoryRepository : IHistoryRepository
{
    private readonly IHistory _history;
    private readonly IHistoryMapper _mapper;

    public HistoryRepository(IHistory history, IHistoryMapper mapper)
    {
        _history = history;
        _mapper = mapper;
    }

    public async Task<List<RecentAlbumDTO>> GetRecentlyAddedAlbums(int maxItems)
    {
        var data = await _history.GetRecentlyAddedAlbums(maxItems);
        return data.Select(_mapper.MapRecentlyAddedAlbum).ToList();
    }

    public async Task<List<RecentAlbumDTO>> GetRecentlyPlayedAlbums(int maxItems)
    {
        var data = await _history.GetRecentlyPlayedAlbums(maxItems);
        return data.Select(_mapper.MapRecentlyPlayedAlbum).ToList();
    }

    public async Task<List<string>> GetRecentTags(int maxItems)
    {
        var data = await _history.GetRecentTags(maxItems);
        return data.Select(d => d.Tag).ToList();
    }

    public async Task<List<RecentTrackDTO>> GetRecentTracks(string username, int maxItems)
    {
        var data = await _history.GetRecentTracks(username, maxItems);
        return data.Select(_mapper.MapRecentTrack).ToList();
    }

    public async Task<List<TopAlbumDTO>> GetTopAlbums(HistoryPeriod period, int maxItems)
    {
        var data = await _history.GetTopAlbums(new GetTopAlbumsParameter { HistoryPeriod = (int)period, MaxItems = maxItems });
        var rank = 1;
        return data.Select(d => _mapper.MapTopAlbum(d, rank++)).ToList();
    }

    public async Task<List<TopArtistDTO>> GetTopArtists(HistoryPeriod period, int maxItems)
    {
        var data = await _history.GetTopArtists(new GetTopArtistsParameter { HistoryPeriod = (int)period, MaxItems = maxItems });
        var rank = 1;
        return data.Select(d => _mapper.MapTopArtist(d, rank++)).ToList();
    }

    public async Task<List<TopTrackDTO>> GetTopTracks(HistoryPeriod period, int maxItems)
    {
        var data = await _history.GetTopTracks(new GetTopTracksParameter { HistoryPeriod = (int)period, MaxItems = maxItems });
        var rank = 1;
        return data.Select(d => _mapper.MapTopTrack(d, rank++)).ToList();
    }

    public async Task ScrobbleTrack(int trackId, string username, DateTime scrobbledAt)
    {
        await _history.InsertScrobble(new InsertScrobbleParameter
        {
            TrackId = trackId, 
            Username = username,
            ScrobbledAt = scrobbledAt
        });
    }

    public async Task SyncScrobbles()
    {
        await _history.SyncScrobbles();
    }

    public async Task UpdateNowPlaying(string username, int trackId, int secondsRemaining)
    {
        await _history.InsertNowPlaying(new InsertNowPlayingParameter
        {
            TrackId = trackId,
            Username = username,
            SecondsRemaining = secondsRemaining
        });
    }
}
