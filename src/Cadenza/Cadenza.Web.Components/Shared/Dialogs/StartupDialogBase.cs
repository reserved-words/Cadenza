using Cadenza.Common.Domain.Extensions;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class StartupDialogBase : DialogBase
    {
        [Inject] public IDispatcher Dispatcher { get; set; }

        [Parameter] public List<ConnectionStartupParameter> Connections { get; set; } = new();

        protected Dictionary<ConnectionType, Connection> ConnectionStates = new Dictionary<ConnectionType, Connection>();

        protected record Connection(string Title, ConnectionState State, string Message);

        protected override void OnInitialized()
        {
            SubscribeToAction<ApplicationStartedAction>(OnApplicationStarted);
            SubscribeToAction<ApplicationStartupProgressAction>(OnApplicationStartupProgress);
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            foreach (var connection in Connections)
            {
                ConnectionStates.Add(connection.Type, new Connection(connection.Type.GetDisplayName(), ConnectionState.None, ""));
            }
            foreach (var connection in Connections)
            {
                Dispatcher.Dispatch(connection.ConnectRequest);
            }
            base.OnParametersSet();
        }

        private void OnApplicationStarted(ApplicationStartedAction action)
        {
            Submit();
        }

        private void OnApplicationStartupProgress(ApplicationStartupProgressAction action)
        {
            ConnectionStates[action.ConnectionType] = ConnectionStates[action.ConnectionType] with
            {
                State = action.State,
                Message = action.Message
            };

            StateHasChanged();
        }
    }
}
