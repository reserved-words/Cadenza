namespace Cadenza.Components.History;

public class HistoryAlbumsBase : HistoryDisplayBase<PlayedAlbum>
{
    protected override async Task<List<PlayedAlbum>> GetItems(HistoryPeriod period)
    {
        return (await History.GetPlayedAlbums(period, 5, 1)).ToList();
    }
}
