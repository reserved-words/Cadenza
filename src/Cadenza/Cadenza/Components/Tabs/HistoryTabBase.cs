using Cadenza.API.Core.LastFM;

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
        TopTracks = (await History.GetPlayedTracks(HistoryPeriod.Week, 5, 1)).ToList();
        TopAlbums = (await History.GetPlayedAlbums(HistoryPeriod.Week, 5, 1)).ToList();
        TopArtists = (await History.GetPlayedArtists(HistoryPeriod.Week, 5, 1)).ToList();
        IsLoading = false;
    }
}