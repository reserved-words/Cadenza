using Cadenza.Core.App;
using Cadenza.Core.Model;

namespace Cadenza.Core;


public class SearchRepositoryCache
{
    private readonly IUpdatesConsumer _updates;

    public SearchRepositoryCache(IUpdatesConsumer updates)
    {
        _updates = updates;
    }

    public event EventHandler UpdateStarted;
    public event EventHandler UpdateCompleted;

    public List<PlayerItem> Items { get; set; } = new ();

    public void StartUpdate()
    {
        UpdateStarted?.Invoke(this, EventArgs.Empty);
    }

    public void FinishUpdate()
    {
        UpdateCompleted?.Invoke(this, EventArgs.Empty);

        _updates.ArtistUpdated += OnArtistUpdated;
    }

    private Task OnArtistUpdated(object sender, Updates.ArtistUpdatedEventArgs e)
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

