using Cadenza.Core.Model;
using Cadenza.Domain;

namespace Cadenza.Database;


public class SearchRepositoryCache
{
    public event EventHandler UpdateStarted;
    public event EventHandler UpdateCompleted;

    private Dictionary<LibrarySource, List<string>> _sourceArtists = new ();

    public List<SourceSearchableItem> Items { get; set; } = new ();

    public void StartUpdate()
    {
        UpdateStarted?.Invoke(this, EventArgs.Empty);
    }

    public void FinishUpdate()
    {
        UpdateCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void AddTracks(LibrarySource source, List<SearchableItem> items)
    {
        Items.AddRange(items.Select(i => new SourceSearchableItem(source, i)));
    }

    public void AddAlbums(LibrarySource source, List<SearchableItem> items)
    {
        Items.AddRange(items.Select(i => new SourceSearchableItem(source, i)));
    }

    public void AddArtists(LibrarySource source, List<SearchableItem> items)
    {
        if (!_sourceArtists.ContainsKey(source))
        {
            _sourceArtists.Add(source, new List<string>());
        }

        foreach (var item in items)
        {
            if (!Items.Any(i => i.Id == item.Id))
            {
                Items.Add(new SourceSearchableItem(null, item));
            }
            _sourceArtists[source].Add(item.Id);
        }
    }

    public void AddPlaylists(List<SearchablePlaylist> items)
    {
        Items.AddRange(items.Select(i => new SourceSearchableItem(null, i)));
    }

    public void Clear()
    {
        Items.Clear();
        _sourceArtists.Clear();
    }
}

