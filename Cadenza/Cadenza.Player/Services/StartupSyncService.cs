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
            PreTask = _repository.Clear
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

        subTask.AddSteps(
            "Fetching artists from source",
            "Copying artists to repository",
            () => source.Value.GetArtists(),
            (r) => _repository.AddArtists(r));

        subTask.AddSteps(
            "Fetching albums from source",
            "Copying albums to repository",
            () => source.Value.GetAlbums(),
            (r) => _repository.AddAlbums(r));

        return subTask;
    }
}