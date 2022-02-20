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

    public async Task ViewPlaying(PlaylistId playlist)
    {
        PlayerItemType? type = null;

        type = playlist.Type switch
        {
            PlaylistType.Album => PlayerItemType.Album,
            PlaylistType.Artist => PlayerItemType.Artist,
            PlaylistType.Track => PlayerItemType.Track,
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
}
