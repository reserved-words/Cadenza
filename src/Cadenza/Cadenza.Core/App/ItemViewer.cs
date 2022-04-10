using Cadenza.Core.Model;
using Cadenza.Core.Playlists;

namespace Cadenza.Core.App;

public class ItemViewer : IItemViewer
{
    private readonly IAppController _app;
    private readonly IIdGenerator _idGenerator;

    public ItemViewer(IAppController app, IIdGenerator idGenerator)
    {
        _app = app;
        _idGenerator = idGenerator;
    }

    public async Task ViewAlbum(Album album)
    {
        var playerItem = new ViewItem(PlayerItemType.Album, album.Id, album.Title);
        await _app.View(playerItem);
    }

    public async Task ViewArtist(Artist artist)
    {
        var playerItem = new ViewItem(PlayerItemType.Artist, artist.Id, artist.Name);
        await _app.View(playerItem);
    }

    public async Task ViewArtist(string id, string name)
    {
        var playerItem = new ViewItem(PlayerItemType.Artist, id, name);
        await _app.View(playerItem);
    }

    public async Task ViewArtist(string name)
    {
        var id = _idGenerator.GenerateId(name);
        await ViewArtist(id, name);
    }

    public async Task ViewGenre(string id)
    {
        var playerItem = new ViewItem(PlayerItemType.Genre, id, id);
        await _app.View(playerItem);
    }

    public async Task ViewGrouping(Grouping id)
    {
        var playerItem = new ViewItem(PlayerItemType.Grouping, id.ToString(), id.GetDisplayName());
        await _app.View(playerItem);
    }

    public async Task ViewPlaying(PlaylistId playlist)
    {
        PlayerItemType? type = playlist.Type switch
        {
            PlaylistType.Album => PlayerItemType.Album,
            PlaylistType.Artist => PlayerItemType.Artist,
            PlaylistType.Track => PlayerItemType.Track,
            PlaylistType.Grouping => PlayerItemType.Grouping,
            PlaylistType.Genre => PlayerItemType.Genre,
            _ => null
        };

        if (!type.HasValue)
            return;

        var playerItem = new ViewItem(type.Value, playlist.Id, playlist.Name);
        await _app.View(playerItem);
    }

    public async Task ViewSearchResult(PlayerItem item)
    {
        var playerItem = new ViewItem(item.Type, item.Id, item.Name);
        await _app.View(playerItem);
    }

    public async Task ViewTrack(Track track)
    {
        var playerItem = new ViewItem(PlayerItemType.Track, track.Id, track.Title);
        await _app.View(playerItem);
    }

    public async Task ViewTrack(string id, string title)
    {
        var playerItem = new ViewItem(PlayerItemType.Track, id, title);
        await _app.View(playerItem);
    }
}
