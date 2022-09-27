using Cadenza.Domain.Model.Updates;
using Cadenza.Utilities.Extensions;
using Cadenza.Utilities.Interfaces;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Database.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Web.Database.Services;

internal class UpdateQueueService : IUpdateQueue
{
    private readonly IHttpHelper _httpHelper;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;

    public UpdateQueueService(IHttpHelper httpHelper, IOptions<DatabaseApiSettings> apiSettings)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
    }

    public Task<List<ItemUpdates>> GetQueuedUpdates()
    {
        return Task.FromResult(new List<ItemUpdates>());
        //var apiBaseUrl = _apiSettings.Value.BaseUrl;
        //var endpoint = _apiSettings.Value.Endpoints.GetUpdates;
        //var url = $"{apiBaseUrl}{endpoint}";
        //return await _httpHelper.Get<List<ItemUpdates>>(url);
    }

    public Task<bool> RemoveQueuedUpdate(ItemUpdates update)
    {
        throw new NotImplementedException();
    }
}
