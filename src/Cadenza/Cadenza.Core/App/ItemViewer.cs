using Cadenza.Core.Model;

namespace Cadenza.Core;

public class ItemViewer : IItemViewer
{
    private readonly IAppController _app;
    
    public ItemViewer(IAppController app)
    {
        _app = app;
    }

    public async Task ViewSearchResult(SourceSearchableItem item)
    {
        var playerItem = new PlayerItem(item.Type, item.Id, item.Name, item.Source);
        await _app.View(playerItem);
    }
}
