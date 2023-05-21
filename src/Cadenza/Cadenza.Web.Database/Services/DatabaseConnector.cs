using Cadenza.Web.Common.Interfaces.Connections;
using Cadenza.Web.Common.Interfaces.Searchbar;
using Cadenza.Web.Common.Interfaces.Startup;

namespace Cadenza.Web.Database.Services;

internal class DatabaseConnector : IConnector
{
    private readonly IConnectionCoordinator _connectorController;
    private readonly IApiHttpHelper _http;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;
    private readonly ISearchCoordinator _searchCoordinator;

    public DatabaseConnector(IConnectionCoordinator connectorController, IApiHttpHelper http, IOptions<DatabaseApiSettings> apiSettings, ISearchCoordinator searchCoordinator)
    {
        _apiSettings = apiSettings;
        _connectorController = connectorController;
        _http = http;
        _searchCoordinator = searchCoordinator;
    }

    public SubTask GetConnectionTask()
    {
        var subTask = new SubTask
        {
            Id = "Database",
            Title = "Connect to Database",
            Steps = new List<TaskStep>(),
            OnError = (ex) => _connectorController.SetStatus(Connector.Database, ConnectorStatus.Errored, ex.Message),
            OnCompleted = () => _connectorController.SetStatus(Connector.Database, ConnectorStatus.Connected)
        };

        subTask.AddStep("Checking connection", Connect);
        subTask.AddStep("Populating library", Populate);
        subTask.AddStep("Populating search items", () => _searchCoordinator.Populate());

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
        await _http.Post(url, null);
    }
}
