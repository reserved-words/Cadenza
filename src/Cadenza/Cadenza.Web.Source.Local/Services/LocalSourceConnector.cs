using Cadenza.State.Actions;

namespace Cadenza.Web.Source.Local.Services;

internal class LocalSourceConnector : IConnector
{
    public StartupTask GetConnectionTask()
    {
        var subTask = new StartupTask
        {
            Connector = Connector.Local,
            Title = "Connect to Local Library",
            InitialAction = new LocalSourceConnectRequest()
        };

        return subTask;
    }
}
