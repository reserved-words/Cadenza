using static Cadenza.Web.Common.Constants;

namespace Cadenza.Web.Actions.Effects;

public class HistoryEffects
{
    private readonly IArtworkApi _artworkApi;
    private readonly IHistoryApi _historyApi;

    public HistoryEffects(IHistoryApi historyApi, IArtworkApi artworkApi)
    {
        _historyApi = historyApi;
        _artworkApi = artworkApi;
    }

    [EffectMethod(typeof(FetchRecentlyPlayedTracksRequest))]
    public async Task HandleFetchRecentlyPlayedTracksRequest(IDispatcher dispatcher)
    {
        var result = await _historyApi.GetRecentlyPlayedTracks(MaxRecentlyPlayedTracks);
        var resultWithImages = AddImages(result);
        dispatcher.Dispatch(new FetchRecentlyPlayedTracksResult(resultWithImages));
    }

    [EffectMethod]
    public async Task HandleFetchTopPlayedArtistsRequest(FetchTopPlayedArtistsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetTopArtists(action.Period, MaxTopPlayedArtists);
        dispatcher.Dispatch(new FetchTopPlayedArtistsResult(action.Period, result.ToList()));
    }

    [EffectMethod]
    public async Task HandleFetchTopPlayedAlbumsRequest(FetchTopPlayedAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetTopAlbums(action.Period, MaxTopPlayedAlbums);
        var resultWithImages = AddImages(result);
        dispatcher.Dispatch(new FetchTopPlayedAlbumsResult(action.Period, resultWithImages));
    }

    [EffectMethod]
    public async Task HandleFetchRecentlyPlayedAlbumsRequest(FetchRecentlyPlayedAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetRecentlyPlayedAlbums(MaxRecentlyPlayedAlbums);
        var resultWithImages = AddImages(result);
        dispatcher.Dispatch(new FetchRecentlyPlayedAlbumsResult(resultWithImages));
    }

    [EffectMethod]
    public async Task HandleFetchRecentlyAddedAlbumsRequest(FetchRecentlyAddedAlbumsRequest action, IDispatcher dispatcher)
    {
        var result = await _historyApi.GetRecentlyAddedAlbums(MaxRecentlyAddedAlbums);
        var resultWithImages = AddImages(result);
        dispatcher.Dispatch(new FetchRecentlyAddedAlbumsResult(resultWithImages));
    }

    private List<RecentAlbumVM> AddImages(List<RecentAlbumVM> albums)
    {
        return albums.Select(a => a with
        {
            ImageUrl = _artworkApi.GetAlbumArtworkUrl(a.Id)
        })
        .ToList();
    }

    private List<TopAlbumVM> AddImages(List<TopAlbumVM> albums)
    {
        return albums.Select(a => a with
        {
            ImageUrl = _artworkApi.GetAlbumArtworkUrl(a.Id)
        })
        .ToList();
    }

    private List<RecentTrackVM> AddImages(List<RecentTrackVM> tracks)
    {
        return tracks.Select(t => t with
        {
            ImageUrl = _artworkApi.GetAlbumArtworkUrl(t.AlbumId)
        })
        .ToList();
    }
}
