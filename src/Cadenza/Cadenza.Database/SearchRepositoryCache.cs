using Cadenza.Domain;

namespace Cadenza.Database;

public class SearchRepositoryCache
{
    public event EventHandler UpdateStarted;
    public event EventHandler UpdateCompleted;

    public Dictionary<LibrarySource, List<string>> SourceArtists = new ();
    public List<SearchableItem> Items { get; set; } = new List<SearchableItem>();

    public void StartUpdate()
    {
        UpdateStarted?.Invoke(this, EventArgs.Empty);
    }

    public void FinishUpdate()
    {
        UpdateCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void AddTracks(List<SearchableTrack> items)
    {
        Items.AddRange(items);
    }

    public void AddAlbums(List<SearchableAlbum> items)
    {
        Items.AddRange(items);
    }

    public void AddArtists(LibrarySource source, List<SearchableArtist> items)
    {
        if (!SourceArtists.ContainsKey(source))
        {
            SourceArtists.Add(source, new List<string>());
        }

        foreach (var item in items)
        {
            if (!Items.Any(i => i.Id == item.Id))
            {
                Items.Add(item);
            }
            SourceArtists[source].Add(item.Id);
        }
    }

    public void AddPlaylists(List<SearchablePlaylist> items)
    {
        Items.AddRange(items);
    }

    public void Clear()
    {
        Items.Clear();
        SourceArtists.Clear();
    }
}

