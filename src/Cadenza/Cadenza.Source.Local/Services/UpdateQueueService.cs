using Cadenza.Core;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Model;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local.Services;

internal class UpdateQueueService : IFileUpdateQueue
{
    private readonly IHttpHelper _httpHelper;
    private readonly IOptions<LocalApiSettings> _apiSettings;

    public UpdateQueueService(IHttpHelper httpHelper, IOptions<LocalApiSettings> apiSettings)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
    }

    public async Task<FileUpdateQueue> GetQueuedUpdates()
    {
        var apiBaseUrl = _apiSettings.Value.BaseUrl;
        var endpoint = _apiSettings.Value.Endpoints.GetUpdates;
        var url = $"{apiBaseUrl}{endpoint}";
        return await _httpHelper.Get<FileUpdateQueue>(url);
    }

    public Task<bool> RemoveQueuedUpdate(ItemPropertyUpdate update)
    {
        throw new NotImplementedException();
    }
}
