using Cadenza.Core.Model;

namespace Cadenza.Core;


public class SearchRepositoryCache
{
    public event EventHandler UpdateStarted;
    public event EventHandler UpdateCompleted;

    private Dictionary<LibrarySource, List<string>> _sourceArtists = new ();
    private Dictionary<LibrarySource, List<string>> _sourceGenres = new();

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
        AddItems(source, items, _sourceArtists);
    }

    public void AddPlaylists(List<PlayerItem> items)
    {
        Items.AddRange(items.Select(i => new SourcePlayerItem(null, i)));
    }

    public void AddGenres(LibrarySource source, List<PlayerItem> items)
    {
        try
        {
            AddItems(source, items, _sourceGenres);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public void AddGroupings(List<PlayerItem> items)
    {
        try
        {
            foreach (var item in items)
            {
                if (!Items.Any(i => i.Id == item.Id))
                {
                    Items.Add(new SourcePlayerItem(null, item));
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public void Clear()
    {
        Items.Clear();
        _sourceArtists.Clear();
        _sourceGenres.Clear();
    }

    private void AddItems(LibrarySource source, List<PlayerItem> items, Dictionary<LibrarySource, List<string>> sourceDictionary)
    {
        if (!sourceDictionary.ContainsKey(source))
        {
            sourceDictionary.Add(source, new List<string>());
        }

        foreach (var item in items)
        {
            if (!Items.Any(i => i.Id == item.Id))
            {
                Items.Add(new SourcePlayerItem(null, item));
            }
            sourceDictionary[source].Add(item.Id);
        }
    }
}

