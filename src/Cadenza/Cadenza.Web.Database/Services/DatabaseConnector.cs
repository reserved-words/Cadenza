using Cadenza.State.Actions;

namespace Cadenza.Web.Database.Services;

internal class DatabaseConnector : IConnector
{
    public StartupTask GetConnectionTask()
    {
        var subTask = new StartupTask
        {
            Connector = Connector.Database,
            Title = "Connect to Database",
            InitialAction = new DatabaseConnectRequest()
        };

        return subTask;
    }
}
