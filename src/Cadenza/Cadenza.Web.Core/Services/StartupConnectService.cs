namespace Cadenza.Web.Core.Services;

internal class StartupConnectService : IStartupTaskService
{
    private readonly IEnumerable<IConnector> _connectors;

    public StartupConnectService(IEnumerable<IConnector> connectors)
    {
        _connectors = connectors;
    }

    public TaskGroup GetStartupTasks()
    {
        var taskGroup = new TaskGroup();

        foreach (var builder in _connectors)
        {
            taskGroup.Tasks.Add(builder.GetConnectionTask());
        }

        return taskGroup;
    }
}
