using Cadenza.Core.Model;

namespace Cadenza.Core;

public class ItemViewer : IItemViewer
{
    private readonly IAppController _app;
    
    public ItemViewer(IAppController app)
    {
        _app = app;
    }

    public async Task ViewAlbum(Album album)
    {
        var playerItem = new ViewItem(PlayerItemType.Album, album.Id, album.Title, album.Source);
        await _app.View(playerItem);
    }

    public async Task ViewSearchResult(SourcePlayerItem item)
    {
        var playerItem = new ViewItem(item.Type, item.Id, item.Name, item.Source);
        await _app.View(playerItem);
    }
}
