namespace Cadenza.UI.Components.History;

public class HistoryArtistsBase : HistoryDisplayBase<PlayedArtist>
{
    protected override async Task<List<PlayedArtist>> GetItems(HistoryPeriod period)
    {
        return (await History.GetPlayedArtists(period, 5, 1)).ToList();
    }
}
