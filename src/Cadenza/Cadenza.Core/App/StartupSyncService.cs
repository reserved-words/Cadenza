namespace Cadenza.Core;

public class StartupSyncService : IStartupSyncService
{
    private readonly IEnumerable<ISourceLibrary> _sources;
    private readonly IMainRepository _repository;

    public StartupSyncService(IEnumerable<ISourceLibrary> sources, IMainRepository repository)
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

    private SubTask GetSyncSourceSubTask(ISourceLibrary source)
    {
        var subTask = new SubTask
        {
            Id = source.Source.ToString(),
            Title = source.Source.ToString(),
            Steps = new List<TaskStep>()
        };

        subTask.AddSteps(
            "Fetching artists from source",
            "Copying artists to repository",
            () => source.GetArtists(),
            (r) => _repository.AddArtists(r));

        subTask.AddSteps(
            "Fetching albums from source",
            "Copying albums to repository",
            () => source.GetAlbums(),
            (r) => _repository.AddAlbums(r));

        return subTask;
    }
}