using Cadenza.Web.Common.Interfaces.Searchbar;

namespace Cadenza.Web.Core.Coordinators;

internal class SearchCoordinator : ISearchCoordinator, ISearchCache
{
    private readonly IMessenger _messenger;

    public SearchCoordinator(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public List<PlayerItem> Items { get; set; } = new();

    public async Task StartUpdate()
    {
        await _messenger.Send(this, new SearchUpdateStartedEventArgs());
    }

    public async Task FinishUpdate()
    {
        await _messenger.Send(this, new SearchUpdateCompletedEventArgs());
        _messenger.Subscribe<ArtistUpdatedEventArgs>(OnArtistUpdated);
    }

    private Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
    {
        AddIfMissing(PlayerItemType.Genre, e.Update.UpdatedItem.Genre);
        return Task.CompletedTask;
    }

    private void AddIfMissing(PlayerItemType type, string id, string name = null)
    {
        if (!Items.Any(i => i.Type == type && i.Id == id))
        {
            Items.Add(new PlayerItem(type, id, name ?? id, null, null, null));
        }
    }

    public void AddTracks(List<PlayerItem> items)
    {
        Items.AddRange(items);
    }

    public void AddAlbums(List<PlayerItem> items)
    {
        Items.AddRange(items);
    }

    public void AddArtists(List<PlayerItem> items)
    {
        Items.AddRange(items);
    }

    public void AddPlaylists(List<PlayerItem> items)
    {
        Items.AddRange(items);
    }

    public void AddGenres(List<PlayerItem> items)
    {
        Items.AddRange(items);
    }

    public void AddGroupings(List<PlayerItem> items)
    {
        Items.AddRange(items);
    }

    public void Clear()
    {
        Items.Clear();
    }
}

