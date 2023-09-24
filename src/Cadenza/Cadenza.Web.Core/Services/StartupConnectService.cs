namespace Cadenza.Web.Core.Services;

internal class StartupConnectService : IStartupTaskService
{
    private readonly IEnumerable<IConnector> _connectors;

    public StartupConnectService(IEnumerable<IConnector> connectors)
    {
        _connectors = connectors;
    }

    public List<SubTask> GetStartupTasks()
    {
        var tasks = new List<SubTask>();

        foreach (var builder in _connectors)
        {
            tasks.Add(builder.GetConnectionTask());
        }

        return tasks;
    }
}
