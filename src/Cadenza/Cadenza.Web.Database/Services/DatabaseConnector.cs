namespace Cadenza.Web.Database.Services;

internal class DatabaseConnector : IConnector
{
    private readonly IConnectorController _connectorController;
    private readonly IHttpHelper _http;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;
    private readonly ISearchSyncService _searchSyncService;

    public DatabaseConnector(IConnectorController connectorController, IHttpHelper http, IOptions<DatabaseApiSettings> apiSettings, ISearchSyncService searchSyncService)
    {
        _apiSettings = apiSettings;
        _connectorController = connectorController;
        _http = http;
        _searchSyncService = searchSyncService;
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
        subTask.AddStep("Populating search items", () => _searchSyncService.PopulateSearchItems());

        return subTask;
    }

    private async Task Connect()
    {
        try
        {
            var response = await _http.Get(GetConnectUrl());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            throw new Exception("Failed to connect to database");
        }
    }

    private async Task Populate()
    {
        try
        {
            var response = await _http.Post(GetPopulateUrl(), null, null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            throw new Exception("Failed to populate local library");
        }
    }

    private string GetConnectUrl()
    {
        return $"{_apiSettings.Value.BaseUrl}{_apiSettings.Value.Endpoints.Connect}";
    }

    private string GetPopulateUrl()
    {
        return $"{_apiSettings.Value.BaseUrl}{_apiSettings.Value.Endpoints.Populate}";
    }
}
