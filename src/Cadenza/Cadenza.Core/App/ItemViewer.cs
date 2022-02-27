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
        var playerItem = new ViewItem(PlayerItemType.Album, album.Id, album.Title, album.Source);
        await _app.View(playerItem);
    }

    public async Task ViewArtist(Artist artist)
    {
        var playerItem = new ViewItem(PlayerItemType.Artist, artist.Id, artist.Name, null);
        await _app.View(playerItem);
    }

    public async Task ViewArtist(string id, string name)
    {
        var playerItem = new ViewItem(PlayerItemType.Artist, id, name, null);
        await _app.View(playerItem);
    }

    public async Task ViewArtist(string name)
    {
        var id = _idGenerator.GenerateId(name);
        await ViewArtist(id, name);
    }

    public async Task ViewGenre(string id)
    {
        var playerItem = new ViewItem(PlayerItemType.Genre, id, id, null);
        await _app.View(playerItem);
    }

    public async Task ViewGrouping(Grouping id)
    {
        var playerItem = new ViewItem(PlayerItemType.Grouping, id.ToString(), id.GetDisplayName(), null);
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

        var playerItem = new ViewItem(type.Value, playlist.Id, playlist.Name, playlist.Source);
        await _app.View(playerItem);
    }

    public async Task ViewSearchResult(SourcePlayerItem item)
    {
        var playerItem = new ViewItem(item.Type, item.Id, item.Name, item.Source);
        await _app.View(playerItem);
    }

    public async Task ViewTrack(Track track)
    {
        var playerItem = new ViewItem(PlayerItemType.Track, track.Id, track.Title, track.Source);
        await _app.View(playerItem);
    }

    public async Task ViewTrack(LibrarySource source, string id, string title)
    {
        var playerItem = new ViewItem(PlayerItemType.Track, id, title, source);
        await _app.View(playerItem);
    }
}
