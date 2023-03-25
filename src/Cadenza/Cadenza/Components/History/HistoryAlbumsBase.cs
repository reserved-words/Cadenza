namespace Cadenza.Components.History;

public class HistoryAlbumsBase : HistoryDisplayBase<PlayedAlbum>
{
    [Parameter]
    public int MaxItems { get; set; }

    protected override async Task<List<PlayedAlbum>> GetItems(HistoryPeriod period)
    {
        return (await History.GetPlayedAlbums(period, MaxItems, 1)).ToList();
    }
}
