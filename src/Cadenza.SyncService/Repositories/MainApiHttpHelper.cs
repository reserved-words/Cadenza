using Cadenza.Common.Interfaces.Utilities;
using Cadenza.Common.Utilities.Services;

namespace Cadenza.SyncService.Repositories;

internal class MainApiHttpHelper : HttpHelper, IMainApiHttpHelper
{
    public MainApiHttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter)
        : base(httpClientFactory, jsonConverter)
    {
    }

    protected override string ClientName => "MainAPI";
}
