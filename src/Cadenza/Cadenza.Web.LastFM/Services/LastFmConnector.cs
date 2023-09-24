using Cadenza.State.Actions;

namespace Cadenza.Web.LastFM.Services;

internal class LastFmConnector : IConnector
{
    public StartupTask GetConnectionTask()
    {
        // TODO: Handle if session key has been revoked - if so need to start Last.FM connection process again
        // (or for now as long as relevant error message displayed could just clear the session key and user can retry)

        var subTask = new StartupTask
        {
            Connector = Connector.LastFm,
            Title = "Connect to Last.FM",
            InitialAction = new LastFmConnectRequest(),
        };

        return subTask;
    }
}
