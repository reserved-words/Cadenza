using Cadenza.Web.Database.Interfaces;
using Cadenza.Web.Database.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.State.Effects;

public class DatabaseConnectionEffects
{
    private readonly IState<DatabaseConnectionState> _state;
    private readonly IApiHttpHelper _httpHelper;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;

    public DatabaseConnectionEffects(IApiHttpHelper httpHelper, IOptions<DatabaseApiSettings> apiSettings, IState<DatabaseConnectionState> state)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
        _state = state;
    }

    [EffectMethod(typeof(DatabaseConnectRequest))]
    public async Task HandleDatabaseConnectRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            var connectionUrl = _apiSettings.Value.Endpoints.Connect;
            await _httpHelper.Get(connectionUrl);
            dispatcher.Dispatch(new DatabasePopulateRequest());
        }
        catch (Exception) // TODO: Error handling
        {
            dispatcher.Dispatch(new DatabaseConnectionErroredAction());
        }
    }

    [EffectMethod(typeof(DatabasePopulateRequest))]
    public async Task HandleDatabasePopulateRequest(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        try
        {
            var populateUrl = _apiSettings.Value.Endpoints.Populate;
            await _httpHelper.Post(populateUrl);
            dispatcher.Dispatch(new DatabaseConnectedAction());
        }
        catch (Exception) // TODO: Error handling
        {
            dispatcher.Dispatch(new DatabaseConnectionErroredAction());
        }
    }

    [EffectMethod(typeof(DatabaseConnectedAction))]
    public Task HandleDatabaseConnectedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        return Task.CompletedTask;
    }

    private void DispatchProgressAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ApplicationStartupProgressAction(Connector.Database, _state.Value.State, _state.Value.Message));
    }
}
