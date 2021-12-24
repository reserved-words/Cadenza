using Cadenza.Database;

namespace Cadenza.Player;

public class StartupSyncService : IStartupSyncService
{
    private readonly Dictionary<LibrarySource, ISourceRepository> _sources;
    private readonly IMainRepository _repository;

    public StartupSyncService(Dictionary<LibrarySource, ISourceRepository> sources, IMainRepository repository)
    {
        _sources = sources;
        _repository = repository;
    }

    public event ProgressEventHandler ProgressChanged;
    public event SyncProgressEventHandler SyncProgressChanged;

    public async Task SyncLibrary(CancellationToken cancellationToken)
    {
        try
        {
            Update("Sync started", cancellationToken);

            // TODO - ADD SYNC LOGIC SO DON'T HAVE TO CLEAR ALL FIRST

            await _repository.Clear();

            var tasks = new List<Task>();
            
            foreach (var source in _sources)
            {
                var task = SyncSource(source, cancellationToken);
                tasks.Add(task);
            }

            Update("Sync in progress", cancellationToken);

            #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.WhenAll(tasks).ContinueWith(async (t) =>
            {
                if (t.IsFaulted)
                {

                }
                else if (t.IsCanceled)
                {
                    Update("Cancelling", CancellationToken.None);
                    await _repository.Clear();
                    ProgressChanged?.Invoke(this, new ProgressEventArgs { Message = "Sync cancelled", Cancelled = true });
                }
                else
                {
                    ProgressChanged?.Invoke(this, new ProgressEventArgs { Message = "Sync complete", Completed = true });
                }
            });
            #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
        catch (OperationCanceledException)
        {
            Update("Cancelling", CancellationToken.None);
            await _repository.Clear();
            ProgressChanged?.Invoke(this, new ProgressEventArgs { Message = "Sync cancelled", Cancelled = true });
        }
    }

    private async Task SyncSource(KeyValuePair<LibrarySource, ISourceRepository> source, CancellationToken cancellationToken)
    {
        try
        {
            Update(source.Key, $"Fetching artists from library", cancellationToken);
            var artists = await source.Value.GetArtists();
            Update(source.Key, $"Copying artists to repository", cancellationToken);
            await _repository.AddArtists(artists);

            Update(source.Key, $"Fetching albums from library", cancellationToken);
            var albums = await source.Value.GetAlbums();
            Update(source.Key, $"Copying albums to repository", cancellationToken);
            await _repository.AddAlbums(albums);
            SyncProgressChanged?.Invoke(this, new SyncProgressEventArgs 
            { 
                Source = source.Key, 
                Message = "Sync complete", 
                Completed = true 
            });
        }
        catch (OperationCanceledException)
        {
            SyncProgressChanged?.Invoke(this, new SyncProgressEventArgs 
            { 
                Source = source.Key,
                Message = "Sync cancelled", 
                Cancelled = true 
            });

            throw;
        }
    }

    private void Update(string message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ProgressChanged?.Invoke(this, new ProgressEventArgs { Message = message });
    }

    private void Update(LibrarySource source, string message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        SyncProgressChanged?.Invoke(this, new SyncProgressEventArgs { Source = source, Message = message });
    }
}