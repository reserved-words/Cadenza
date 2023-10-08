using Cadenza.Web.Database.Interfaces;
using Cadenza.Web.Database.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cadenza.Web.Actions.Effects;

public class DatabaseConnectionEffects
{
    private readonly IState<DatabaseConnectionState> _state;
    private readonly IApiHttpHelper _httpHelper;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;
    private readonly ILogger<DatabaseConnectionEffects> _logger;

    public DatabaseConnectionEffects(IApiHttpHelper httpHelper, IOptions<DatabaseApiSettings> apiSettings, IState<DatabaseConnectionState> state, ILogger<DatabaseConnectionEffects> logger)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
        _state = state;
        _logger = logger;
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to database");
            dispatcher.Dispatch(new DatabaseConnectionFailedAction());
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to populate database");
            dispatcher.Dispatch(new DatabaseConnectionFailedAction());
        }
    }

    [EffectMethod(typeof(DatabaseConnectionFailedAction))]
    public Task HandleDatabaseConnectionFailedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        return Task.CompletedTask;
    }

    [EffectMethod(typeof(DatabaseConnectedAction))]
    public Task HandleDatabaseConnectedAction(IDispatcher dispatcher)
    {
        DispatchProgressAction(dispatcher);
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        dispatcher.Dispatch(new FetchGroupingsRequest());
        dispatcher.Dispatch(new FetchPlaylistHistoryAlbumsRequest());
        dispatcher.Dispatch(new FetchPlaylistHistoryTagsRequest());
        return Task.CompletedTask;
    }

    private void DispatchProgressAction(IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ApplicationStartupProgressAction(ConnectionType.Database, _state.Value.State, _state.Value.Message));
    }
}
