using Cadenza.Common;

namespace Cadenza;

public class StartupSyncService : IStartupSyncService
{
    private readonly ISearchSyncService _searchSyncService;

    public StartupSyncService(ISearchSyncService searchSyncService)
    {
        _searchSyncService = searchSyncService;
    }

    public TaskGroup GetStartupTasks()
    {
        var taskGroup = new TaskGroup();

        taskGroup.Tasks.Add(GetSearchSyncSubTask());

        return taskGroup;
    }

    private SubTask GetSearchSyncSubTask()
    {
        var subTask = new SubTask
        {
            Id = "SearchSync",
            Title = "Sync Search Items",
            Steps = new List<TaskStep>()
        };

        subTask.Steps.Add(new TaskStep
        {
            Caption = "Syncing search items",
            Task = async (o) =>
            {
                await _searchSyncService.PopulateSearchItems();
                return true;
            }
        });

        return subTask;
    }
}
