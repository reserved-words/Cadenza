using Cadenza.Core.Common;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Tasks;
using Cadenza.Source.Local;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza
{
    internal class LocalConnectionTaskBuilder : IConnectionTaskBuilder
    {
        private readonly IConnectorController _connectorController;
        private readonly IOptions<LocalApiSettings> _apiSettings;
        private readonly IHttpHelper _http;
        private readonly ISearchSyncService _searchSyncService;

        public LocalConnectionTaskBuilder(IConnectorController connectorController, IOptions<LocalApiSettings> apiSettings, IHttpHelper http, ISearchSyncService searchSyncService)
        {
            _connectorController = connectorController;
            _apiSettings = apiSettings;
            _http = http;
            _searchSyncService = searchSyncService;
        }

        public SubTask GetConnectionTask()
        {
            var subTask = new SubTask
            {
                Id = "Local",
                Title = "Connect to Local Library",
                Steps = new List<TaskStep>(),
                OnError = (ex) => _connectorController.SetStatus(Connector.Local, ConnectorStatus.Errored, ex.Message),
                OnCompleted = () => _connectorController.SetStatus(Connector.Local, ConnectorStatus.Connected)
            };

            subTask.AddStep("Checking connection", Connect);
            subTask.AddStep("Populating library", Populate);
            subTask.AddStep("Populating search items", () => _searchSyncService.PopulateSearchItems());

            return subTask;
        }

        private async Task Connect()
        {
            try
            {
                var response = await _http.Get(GetConnectUrl());

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception("Failed to connect to Local API");
            }
        }

        private async Task Populate()
        {
            try
            {
                var response = await _http.Post(GetPopulateUrl(), null, null);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception("Failed to populate local library");
            }
        }

        private string GetConnectUrl()
        {
            return $"{_apiSettings.Value.BaseUrl}{_apiSettings.Value.Endpoints.Connect}";
        }

        private string GetPopulateUrl()
        {
            return $"{_apiSettings.Value.BaseUrl}{_apiSettings.Value.Endpoints.Populate}";
        }
    }
}
