namespace Cadenza.Components.History;

public class HistoryTracksBase : HistoryDisplayBase<PlayedTrack>
{
    [Parameter]
    public int MaxItems { get; set; }

    protected override async Task<List<PlayedTrack>> GetItems(HistoryPeriod period)
    {
        return (await History.GetPlayedTracks(period, MaxItems, 1)).ToList();
    }
}
