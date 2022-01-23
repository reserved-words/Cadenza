using Cadenza.Common;

namespace Cadenza;

public class HistoryTabBase : ComponentBase
{
    [Inject]
    public IHistory History { get; set; }

    public List<RecentTrack> RecentTracks { get; set; }

    public List<PlayedTrack> TopTracks { get; set; }

    public List<PlayedAlbum> TopAlbums { get; set; }

    public List<PlayedArtist> TopArtists { get; set; }

    public bool IsLoading { get; set; } = true;

    protected override async Task OnParametersSetAsync()
    {
        IsLoading = true;
        RecentTracks = (await History.GetRecentTracks(20, 1)).ToList();
        TopTracks = (await History.GetTopTracks(HistoryPeriod.Week, 5, 1)).ToList();
        TopAlbums = (await History.GetTopAlbums(HistoryPeriod.Week, 5, 1)).ToList();
        TopArtists = (await History.GetTopArtists(HistoryPeriod.Week, 5, 1)).ToList();
        IsLoading = false;
    }
}