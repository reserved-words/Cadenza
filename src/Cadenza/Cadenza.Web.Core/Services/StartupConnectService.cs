namespace Cadenza.Web.Core.Services;

internal class StartupConnectService : IStartupConnectService
{
    private readonly IStoreSetter _storeSetter;
    private readonly IEnumerable<IConnector> _connectors;

    public StartupConnectService(IStoreSetter storeSetter, IEnumerable<IConnector> connectors)
    {
        _storeSetter = storeSetter;
        _connectors = connectors;
    }

    public TaskGroup GetStartupTasks()
    {
        var taskGroup = new TaskGroup
        {
            PreTask = ClearSessionData
        };

        foreach (var builder in _connectors)
        {
            taskGroup.Tasks.Add(builder.GetConnectionTask());
        }

        return taskGroup;
    }

    private async Task ClearSessionData()
    {
        await _storeSetter.Clear(StoreKey.CurrentTrackSource);
        await _storeSetter.Clear(StoreKey.CurrentTrack);
    }
}
