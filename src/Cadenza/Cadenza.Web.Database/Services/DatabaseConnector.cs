using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Web.Database.Services;

internal class DatabaseConnector : IConnector
{
    private readonly IDispatcher _dispatcher;
    private readonly IApiHttpHelper _http;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;

    public DatabaseConnector(IDispatcher dispatcher, IApiHttpHelper http, IOptions<DatabaseApiSettings> apiSettings)
    {
        _apiSettings = apiSettings;
        _dispatcher = dispatcher;
        _http = http;
    }

    public StartupTask GetConnectionTask()
    {
        var subTask = new StartupTask
        {
            Id = "Database",
            Title = "Connect to Database",
            Steps = new List<TaskStep>(),
            OnError = (ex) => _dispatcher.Dispatch(new ConnectorStatusUpdateRequest(Connector.Database, ConnectorStatus.Errored, ex)),
            OnCompleted = () => _dispatcher.Dispatch(new ConnectorStatusUpdateRequest(Connector.Database, ConnectorStatus.Connected, null))
        };

        subTask.AddStep("Checking connection", Connect);
        subTask.AddStep("Populating library", Populate);

        return subTask;
    }

    private async Task Connect()
    {
        var url = _apiSettings.Value.Endpoints.Connect;
        await _http.Get(url);
    }

    private async Task Populate()
    {
        var url = _apiSettings.Value.Endpoints.Populate;
        await _http.Post(url);
    }
}
