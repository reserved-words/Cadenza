using Cadenza.Web.Common.Interfaces.Coordinators;

namespace Cadenza.Web.Source.Local.Services;

internal class LocalSourceConnector : IConnector
{
    private readonly IConnectorController _connectorController;
    private readonly IHttpHelper _http;
    private readonly IOptions<LocalApiSettings> _apiSettings;
    private readonly IUrl _url;

    public LocalSourceConnector(IConnectorController connectorController, IOptions<LocalApiSettings> apiSettings, IHttpHelper http, IUrl url)
    {
        _apiSettings = apiSettings;
        _connectorController = connectorController;
        _http = http;
        _url = url;
    }

    public SubTask GetConnectionTask()
    {
        var subTask = new SubTask
        {
            Id = "Local",
            Title = "Connect to Local Library",
            Steps = new List<TaskStep>(),
            OnError = (ex) => _connectorController.SetStatus(Connector.Local, ConnectorStatus.Errored, ex.Message),
            OnCompleted = () => _connectorController.SetStatus(Connector.Local, ConnectorStatus.Connected)
        };

        subTask.AddStep("Checking connection", Connect);

        return subTask;
    }

    public async Task Connect()
    {
        var url = _url.Build(_apiSettings.Value.BaseUrl, _apiSettings.Value.Endpoints.Connect);
        await _http.GetString(url);
    }
}
