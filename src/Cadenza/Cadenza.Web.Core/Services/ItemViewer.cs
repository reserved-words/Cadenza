using Cadenza.Web.Common.Extensions;
using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Web.Core.Services;

internal class ItemViewer : IItemViewer
{
    private readonly IViewCoordinator _controller;

    public ItemViewer(IViewCoordinator controller)
    {
        _controller = controller;
    }

    public async Task ViewAlbum(int id, string title)
    {
        var playerItem = new ViewItem(PlayerItemType.Album, id.ToString(), title);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewArtist(int id, string name)
    {
        var playerItem = new ViewItem(PlayerItemType.Artist, id.ToString(), name);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewGenre(string id)
    {
        var playerItem = new ViewItem(PlayerItemType.Genre, id, id);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewGrouping(Grouping grouping)
    {
        var playerItem = new ViewItem(PlayerItemType.Grouping, grouping.Id.ToString(), grouping.Name);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewPlaying(PlaylistId playlist)
    {
        var type = playlist.Type.GetItemType();

        if (!type.HasValue)
            return;

        var playerItem = new ViewItem(type.Value, playlist.Id, playlist.Name);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewSearchResult(PlayerItem item)
    {
        var playerItem = new ViewItem(item.Type, item.Id, item.Name);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewTag(string id)
    {
        var playerItem = new ViewItem(PlayerItemType.Tag, id, id);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewTrack(int id, string title)
    {
        var playerItem = new ViewItem(PlayerItemType.Track, id.ToString(), title);
        await _controller.RequestItem(playerItem);
    }
}
