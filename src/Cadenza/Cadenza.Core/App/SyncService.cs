namespace Cadenza.Core;

public class SyncService : ISyncService
{
    private readonly IEnumerable<ISourceLibrary> _sources;
    private readonly IMainRepository _repository;

    public SyncService(IEnumerable<ISourceLibrary> sources, IMainRepository repository)
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
            (r) => _repository.AddArtists(r.ToList()));

        subTask.AddSteps(
            "Fetching albums from source",
            "Copying albums to repository",
            () => source.GetAlbums(),
            (r) => _repository.AddAlbums(r.ToList()));

        subTask.AddSteps(
            "Fetching tracks from source",
            "Copying tracks to repository",
            () => source.GetAllTracks(),
            (r) => _repository.AddTracks(source.Source, r.ToList()));

        return subTask;
    }
}