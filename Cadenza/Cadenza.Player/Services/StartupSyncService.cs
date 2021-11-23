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
        // For now maybe need to clear all repositories before starting sync - later change to a sync process

        try
        {
            Update("Sync started", cancellationToken);
            await _repository.Clear();

            foreach (var source in _sources)
            {
                Update($"Fetching artists from {source.Key} library", cancellationToken);
                var artists = await source.Value.GetArtists();
                Update($"Copying artists from {source.Key} library to repository", cancellationToken);
                await _repository.AddArtists(artists);

                Update($"Fetching albums from {source.Key} library", cancellationToken);
                var albums = await source.Value.GetAlbums();
                Update($"Copying albums from {source.Key} library to repository", cancellationToken);
                await _repository.AddAlbums(albums);

                Update($"Fetching tracks from {source.Key} library", cancellationToken);
                var tracks = await source.Value.GetTracks();
                Update($"Copying tracks from {source.Key} library to repository", cancellationToken);
                await _repository.AddTracks(tracks);

                Update($"Fetching album track links from {source.Key} library", cancellationToken);
                var albumTrackLinks = await source.Value.GetAlbumTrackLinks();
                Update($"Copying album track links from {source.Key} library to repository", cancellationToken);
                await _repository.AddAlbumTrackLinks(albumTrackLinks);
            }

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