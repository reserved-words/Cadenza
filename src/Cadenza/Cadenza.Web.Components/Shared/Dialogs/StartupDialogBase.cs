using Cadenza.State.Actions;
using Cadenza.Web.Common.Model;
using Fluxor;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class StartupDialogBase : DialogBase
    {
        [Inject] public IDispatcher Dispatcher { get; set; }

        [Parameter] public List<ConnectionStartupParameter> Connections { get; set; } = new();


        protected override void OnInitialized()
        {
            SubscribeToAction<ApplicationStartedAction>(OnApplicationStarted);
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
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
    }
}
