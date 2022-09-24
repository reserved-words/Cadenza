using Cadenza.Domain.Models;
using Cadenza.Utilities.Extensions;
using Cadenza.Utilities.Interfaces;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Common.Model;
using Cadenza.Web.Database.Settings;
using Microsoft.Extensions.Options;

namespace Cadenza.Web.Database.Services;

internal class UpdateQueueService : IFileUpdateQueue
{
    private readonly IHttpHelper _httpHelper;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;

    public UpdateQueueService(IHttpHelper httpHelper, IOptions<DatabaseApiSettings> apiSettings)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
    }

    public async Task<List<ItemPropertyUpdate>> GetQueuedUpdates()
    {
        var apiBaseUrl = _apiSettings.Value.BaseUrl;
        var endpoint = _apiSettings.Value.Endpoints.GetUpdates;
        var url = $"{apiBaseUrl}{endpoint}";
        return await _httpHelper.Get<List<ItemPropertyUpdate>>(url);
    }

    public Task<bool> RemoveQueuedUpdate(ItemPropertyUpdate update)
    {
        throw new NotImplementedException();
    }
}
