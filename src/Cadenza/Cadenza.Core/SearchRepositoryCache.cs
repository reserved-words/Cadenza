using Cadenza.Core.Model;

namespace Cadenza.Core;


public class SearchRepositoryCache
{
    public event EventHandler UpdateStarted;
    public event EventHandler UpdateCompleted;

    private Dictionary<LibrarySource, List<string>> _sourceArtists = new ();

    public List<SourcePlayerItem> Items { get; set; } = new ();

    public void StartUpdate()
    {
        UpdateStarted?.Invoke(this, EventArgs.Empty);
    }

    public void FinishUpdate()
    {
        UpdateCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void AddTracks(LibrarySource source, List<PlayerItem> items)
    {
        Items.AddRange(items.Select(i => new SourcePlayerItem(source, i)));
    }

    public void AddAlbums(LibrarySource source, List<PlayerItem> items)
    {
        Items.AddRange(items.Select(i => new SourcePlayerItem(source, i)));
    }

    public void AddArtists(LibrarySource source, List<PlayerItem> items)
    {
        if (!_sourceArtists.ContainsKey(source))
        {
            _sourceArtists.Add(source, new List<string>());
        }

        foreach (var item in items)
        {
            if (!Items.Any(i => i.Id == item.Id))
            {
                Items.Add(new SourcePlayerItem(null, item));
            }
            _sourceArtists[source].Add(item.Id);
        }
    }

    public void AddPlaylists(List<SearchablePlaylist> items)
    {
        Items.AddRange(items.Select(i => new SourcePlayerItem(null, i)));
    }

    public void Clear()
    {
        Items.Clear();
        _sourceArtists.Clear();
    }
}

