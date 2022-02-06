namespace Cadenza.Library;

public abstract class Library : ILibrary
{
    private readonly ISourceFactory _sourceFactory;
    private readonly IMerger _merger;

    private FullLibrary _library;

    public Library(ISourceFactory sourceFactory, IMerger merger)
    {
        _sourceFactory = sourceFactory;
        _merger = merger;
    }

    public abstract LibrarySource Source { get; }

    public bool IsPopulated => _library != null;

    public async Task<FullLibrary> Get()
    {
        if (!IsPopulated)
        {
            await Populate();
        }

        return _library;
    }

    public async Task Populate()
    {
        // todo - add error handling

        var sources = _sourceFactory.GetSources();

        var tasks = sources.Select(s => s.Get());

        _library = await Task
            .WhenAll(tasks)
            .ContinueWith(t =>
            {
                var cancelled = tasks.Where(t => t.IsCanceled).ToList();
                var completed = tasks.Where(t => t.IsCompleted).ToList();
                var faulted = tasks.Where(t => t.IsFaulted).ToList();

                var results = completed.Select(task => task.Result);

                return Combine(results);
            });
    }

    private FullLibrary Combine(IEnumerable<FullLibrary> sources)
    {
        var result = new FullLibrary();
        var mergeMode = MergeMode.ReplaceIfUpdateIsNotEmpty;

        foreach (var source in sources)
        {
            result.Artists = Merge(result.Artists, source.Artists, a => a.Id, (a1, a2) => _merger.MergeArtist(a1, a2, mergeMode));
            result.Albums = Merge(result.Albums, source.Albums, a => a.Id, (a1, a2) => _merger.MergeAlbum(a1, a2, mergeMode));
            result.Tracks = Merge(result.Tracks, source.Tracks, a => a.Id, (a1, a2) => _merger.MergeTrack(a1, a2, mergeMode));
            result.AlbumTrackLinks = Merge(result.AlbumTrackLinks, source.AlbumTrackLinks, a => a.TrackId, (a1, a2) => _merger.MergeAlbumTrackLink(a1, a2, mergeMode));
        }

        return result;
    }

    private static List<T> Merge<T>(List<T> existing, List<T> update, Func<T, string> id, Action<T, T> merge)
    {
        if (existing == null)
        {
            return update;
        }
        else
        {
            foreach (var updateItem in update)
            {
                var existingItem = existing.SingleOrDefault(i => id(i) == id(updateItem));
                if (existingItem != null)
                {
                    merge(existingItem, updateItem);
                }
                else
                {
                    existing.Add(updateItem);
                }
            }

            return existing;
        }
    }
}