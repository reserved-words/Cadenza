using Cadenza.Web.Common.Events;
using Cadenza.Web.Common.Tasks;
using Cadenza.Web.Database.Interfaces;
using Cadenza.Web.Database.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.State.Effects;

public class DatabaseConnectionEffects
{
    private readonly IApiHttpHelper _httpHelper;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;

    public DatabaseConnectionEffects(IApiHttpHelper httpHelper, IOptions<DatabaseApiSettings> apiSettings)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
    }

    [EffectMethod(typeof(DatabaseConnectRequest))]
    public async Task HandleDatabaseConnectRequest(IDispatcher dispatcher)
    {
        try
        {
            var connectionUrl = _apiSettings.Value.Endpoints.Connect;
            dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Local, "Connecting", TaskState.Running));
            await _httpHelper.Get(connectionUrl);
            dispatcher.Dispatch(new DatabasePopulateRequest());
        }
        catch (Exception) // TODO: Error handling
        {
            dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Database, "Failed to connect", TaskState.Errored));
        }
    }

    [EffectMethod(typeof(DatabasePopulateRequest))]
    public async Task HandleDatabasePopulateRequest(IDispatcher dispatcher)
    {
        try
        {
            var populateUrl = _apiSettings.Value.Endpoints.Populate;
            dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Database, "Populating", TaskState.Running));
            await _httpHelper.Post(populateUrl);
            dispatcher.Dispatch(new DatabaseConnectedAction());
        }
        catch (Exception) // TODO: Error handling
        {
            dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Database, "Failed to connect", TaskState.Errored));
        }
    }

    [EffectMethod(typeof(DatabaseConnectedAction))]
    public Task HandleDatabaseConnectedAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Database, "Fetching search items", TaskState.Running));
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        dispatcher.Dispatch(new StartupTaskProgressAction(Connector.Database, "Connection succeeded", TaskState.Completed));
        return Task.CompletedTask;
    }
}
