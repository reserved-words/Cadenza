using Cadenza.Common;
using Cadenza.Core;

namespace Cadenza;

public class HistoryTabBase : ComponentBase
{
    [Inject]
    public IHistory History { get; set; }

    public List<PlayedTrack> TopTracks { get; set; }

    public List<PlayedAlbum> TopAlbums { get; set; }

    public List<PlayedArtist> TopArtists { get; set; }

    public bool IsLoading { get; set; } = true;

    protected override async Task OnParametersSetAsync()
    {
        IsLoading = true;
        TopTracks = (await History.GetTopTracks(HistoryPeriod.Week, 5, 1)).ToList();
        TopAlbums = (await History.GetTopAlbums(HistoryPeriod.Week, 5, 1)).ToList();
        TopArtists = (await History.GetTopArtists(HistoryPeriod.Week, 5, 1)).ToList();
        IsLoading = false;
    }
}