using Cadenza.State.Actions;
using Cadenza.Web.Source.Local.Interfaces;
using Fluxor;

namespace Cadenza.Web.Source.Local.Services;

internal class LocalSourceConnector : IConnector
{
    private readonly IDispatcher _dispatcher;
    private readonly ILocalHttpHelper _httpHelper;
    private readonly IOptions<LocalApiSettings> _apiSettings;

    public LocalSourceConnector(IDispatcher dispatcher, IOptions<LocalApiSettings> apiSettings, ILocalHttpHelper httpHelper)
    {
        _apiSettings = apiSettings;
        _dispatcher = dispatcher;
        _httpHelper = httpHelper;
    }

    public SubTask GetConnectionTask()
    {
        var subTask = new SubTask
        {
            Id = "Local",
            Title = "Connect to Local Library",
            Steps = new List<TaskStep>(),
            OnError = (ex) => _dispatcher.Dispatch(new ConnectorStatusUpdateRequest(Connector.Local, ConnectorStatus.Errored, ex)),
            OnCompleted = () => _dispatcher.Dispatch(new ConnectorStatusUpdateRequest(Connector.Local, ConnectorStatus.Connected, null))
        };

        subTask.AddStep("Checking connection", Connect);

        return subTask;
    }

    public async Task Connect()
    {
        await _httpHelper.Get(_apiSettings.Value.Endpoints.Connect);
    }
}
