using Cadenza.Common;
using Cadenza.Source.Local;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza
{
    public class LocalLibraryConnectionTaskBuilder : IConnectionTaskBuilder
    {
        private readonly IConnectorController _connectorController;
        private readonly IOptions<LocalApiSettings> _apiSettings;
        private readonly IHttpHelper _http;

        public LocalLibraryConnectionTaskBuilder(IConnectorController connectorController, IOptions<LocalApiSettings> apiSettings, IHttpHelper http)
        {
            _connectorController = connectorController;
            _apiSettings = apiSettings;
            _http = http;
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

            return subTask;
        }

        private string GetConnectUrl()
        {
            return $"{_apiSettings.Value.BaseUrl}{_apiSettings.Value.Endpoints.Connect}";
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
    }
}
