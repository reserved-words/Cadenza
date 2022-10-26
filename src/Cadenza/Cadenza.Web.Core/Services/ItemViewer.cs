using Cadenza.Web.Common.Interfaces.View;

namespace Cadenza.Web.Core.Services;

internal class ItemViewer : IItemViewer
{
    private readonly IViewCoordinator _controller;
    private readonly IIdGenerator _idGenerator;

    public ItemViewer(IViewCoordinator controller, IIdGenerator idGenerator)
    {
        _controller = controller;
        _idGenerator = idGenerator;
    }

    public async Task ViewAlbum(string id, string title)
    {
        var playerItem = new ViewItem(PlayerItemType.Album, id, title);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewArtist(string id, string name)
    {
        var playerItem = new ViewItem(PlayerItemType.Artist, id, name);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewArtist(string name)
    {
        var id = _idGenerator.GenerateId(name);
        await ViewArtist(id, name);
    }

    public async Task ViewGenre(string id)
    {
        var playerItem = new ViewItem(PlayerItemType.Genre, id, id);
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewGrouping(Grouping id)
    {
        var playerItem = new ViewItem(PlayerItemType.Grouping, id.ToString(), id.GetDisplayName());
        await _controller.RequestItem(playerItem);
    }

    public async Task ViewPlaying(PlaylistId playlist)
    {
        PlayerItemType? type = playlist.Type switch
        {
            PlaylistType.Album => PlayerItemType.Album,
            PlaylistType.Artist => PlayerItemType.Artist,
            PlaylistType.Genre => PlayerItemType.Genre,
            PlaylistType.Grouping => PlayerItemType.Grouping,
            PlaylistType.Tag => PlayerItemType.Tag,
            PlaylistType.Track => PlayerItemType.Track,
            _ => null
        };

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

    public async Task ViewTrack(string id, string title)
    {
        var playerItem = new ViewItem(PlayerItemType.Track, id, title);
        await _controller.RequestItem(playerItem);
    }
}
