namespace Cadenza.Components.History;

public class HistoryArtistsBase : HistoryDisplayBase<PlayedArtist>
{
    [Parameter]
    public int MaxItems { get; set; }

    protected override async Task<List<PlayedArtist>> GetItems(HistoryPeriod period)
    {
        return (await History.GetPlayedArtists(period, MaxItems, 1)).ToList();
    }
}
