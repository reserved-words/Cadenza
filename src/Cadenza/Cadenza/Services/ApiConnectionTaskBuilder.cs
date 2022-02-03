using Cadenza.API.Core;
using Cadenza.Common;

namespace Cadenza
{
    public class ApiConnectionTaskBuilder : IConnectionTaskBuilder
    {
        private readonly IConnectorController _connectorController;
        private readonly IConnectionChecker _connectionChecker;

        public ApiConnectionTaskBuilder(IConnectorController connectorController, IConnectionChecker connectionChecker)
        {
            _connectorController = connectorController;
            _connectionChecker = connectionChecker;
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

        private async Task Connect()
        {
            await _connectionChecker.CheckConnection();
        }
    }
}
