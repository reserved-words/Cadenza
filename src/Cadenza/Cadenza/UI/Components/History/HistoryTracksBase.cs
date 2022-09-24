using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.History;

namespace Cadenza.UI.Components.History;

public class HistoryTracksBase : HistoryDisplayBase<PlayedTrack>
{
    protected override async Task<List<PlayedTrack>> GetItems(HistoryPeriod period)
    {
        return (await History.GetPlayedTracks(period, 5, 1)).ToList();
    }
}
