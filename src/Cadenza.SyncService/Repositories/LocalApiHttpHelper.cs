using Cadenza.Common.Interfaces.Utilities;
using Cadenza.Common.Utilities.Services;

namespace Cadenza.SyncService.Repositories;

internal class LocalApiHttpHelper : HttpHelper, ILocalHttpHelper
{
    public LocalApiHttpHelper(IHttpClientFactory httpClientFactory, IJsonConverter jsonConverter)
        : base(httpClientFactory, jsonConverter)
    {
    }

    protected override string ClientName => "LocalAPI";
}
