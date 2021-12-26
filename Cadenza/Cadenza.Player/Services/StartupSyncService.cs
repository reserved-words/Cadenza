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

    public TaskGroup GetLibrarySyncTasks()
    {
        var taskGroup = new TaskGroup
        {
            PreTask = new TaskStep
            {
                Task = _repository.Clear(),
                Caption = "Clearing repository"
            }
        };

        foreach (var source in _sources)
        {
            taskGroup.Tasks.Add(GetSyncSourceSubTask(source));
        }

        return taskGroup;
    }

    private SubTask GetSyncSourceSubTask(KeyValuePair<LibrarySource, ISourceRepository> source)
    {
        var subTask = new SubTask
        {
            Id = source.Key.ToString(),
            Title = source.Key.ToString(),
            Steps = new List<TaskStep>()
        };

        subTask.AddStep("Copying artists from source to repository", CopyArtists(source.Value));
        subTask.AddStep("Copying albums from source to repository", CopyAlbums(source.Value));

        return subTask;
    }

    private async Task CopyArtists(ISourceRepository source)
    {
        var artists = await source.GetArtists();
        await _repository.AddArtists(artists);
    }

    private async Task CopyAlbums(ISourceRepository source)
    {
        var albums = await source.GetAlbums();
        await _repository.AddAlbums(albums);
    }
}