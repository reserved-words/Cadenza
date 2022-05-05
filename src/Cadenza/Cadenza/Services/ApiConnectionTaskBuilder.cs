using Cadenza.Core.Common;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Tasks;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.Services
{
    public class ApiConnectionTaskBuilder : IConnectionTaskBuilder
    {
        private readonly CoreApiSettings _apiSettings;
        private readonly IConnectorController _connectorController;
        private readonly IUrl _url;
        private readonly IHttpHelper _http;

        public ApiConnectionTaskBuilder(IConnectorController connectorController, IUrl url, IHttpHelper http, IOptions<CoreApiSettings> apiSettings)
        {
            _connectorController = connectorController;
            _url = url;
            _http = http;
            _apiSettings = apiSettings.Value;
        }

        public SubTask GetConnectionTask()
        {
            var subTask = new SubTask
            {
                Id = "API",
                Title = "Connect to Cadenza API",
                Steps = new List<TaskStep>(),
                OnError = (ex) => _connectorController.SetStatus(Connector.API, ConnectorStatus.Errored, ex.Message),
                OnCompleted = () => _connectorController.SetStatus(Connector.API, ConnectorStatus.Connected)
            };

            subTask.AddStep("Checking connection", Connect);

            return subTask;
        }

        public async Task Connect()
        {
            var url = _url.Build(_apiSettings.BaseUrl, _apiSettings.Endpoints.Connect);
            await _http.GetString(url);
        }
    }
}
