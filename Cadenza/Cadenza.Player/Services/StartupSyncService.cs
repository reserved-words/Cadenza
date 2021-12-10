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

    public async Task SyncLibrary(CancellationToken cancellationToken)
    {
        try
        {
            Update("Sync started", cancellationToken);

            // UNCOMMENT TO SYNC ON STARTUP
            // TODO - ADD BUTTON TO SYNC ON DEMAND
            // TODO - ADD SYNC LOGIC SO DON'T HAVE TO CLEAR ALL FIRST

            //await _repository.Clear();

            //foreach (var source in _sources)
            //{
            //    Update($"Fetching artists from {source.Key} library", cancellationToken);
            //    var artists = await source.Value.GetArtists();
            //    Update($"Copying artists from {source.Key} library to repository", cancellationToken);
            //    await _repository.AddArtists(artists);

            //    Update($"Fetching albums from {source.Key} library", cancellationToken);
            //    var albums = await source.Value.GetAlbums();
            //    Update($"Copying albums from {source.Key} library to repository", cancellationToken);
            //    await _repository.AddAlbums(albums);
            //}

            ProgressChanged?.Invoke(this, new ProgressEventArgs { Message = "Sync complete", Completed = true });

        }
        catch (OperationCanceledException)
        {
            Update("Cancelling", CancellationToken.None);
            await _repository.Clear();
            ProgressChanged?.Invoke(this, new ProgressEventArgs { Message = "Sync cancelled", Cancelled = true });
        }
        catch (Exception ex)
        {

        }
    }

    private void Update(string message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ProgressChanged?.Invoke(this, new ProgressEventArgs { Message = message });
    }
}