using Cadenza.Web.Common.Interfaces.Connections;
using Cadenza.Web.Common.Interfaces.Startup;
using Cadenza.Web.Source.Local.Interfaces;

namespace Cadenza.Web.Source.Local.Services;

internal class LocalSourceConnector : IConnector
{
    private readonly IConnectionCoordinator _connectorController;
    private readonly ILocalHttpHelper _httpHelper;
    private readonly IOptions<LocalApiSettings> _apiSettings;

    public LocalSourceConnector(IConnectionCoordinator connectorController, IOptions<LocalApiSettings> apiSettings, ILocalHttpHelper httpHelper)
    {
        _apiSettings = apiSettings;
        _connectorController = connectorController;
        _httpHelper = httpHelper;
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
        await _httpHelper.Get(_apiSettings.Value.Endpoints.Connect);
    }
}
