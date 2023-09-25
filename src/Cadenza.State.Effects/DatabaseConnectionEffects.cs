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
        dispatcher.Dispatch(new SearchItemsUpdateRequest());
        return Task.CompletedTask;
    }
}
