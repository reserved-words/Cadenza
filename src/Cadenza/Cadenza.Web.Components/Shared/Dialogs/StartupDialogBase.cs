using Cadenza.State.Actions;
using Fluxor;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class StartupDialogBase : DialogBase
    {
        [Inject] public IDispatcher Dispatcher { get; set; }

        protected bool Started;

        private readonly List<StartupTask> _tasks = new();

        protected override void OnInitialized()
        {
            SubscribeToAction<ApplicationStartedAction>(OnApplicationStarted);
            AddConnector(Connector.LastFm, new LastFmConnectRequest());
            AddConnector(Connector.Database, new DatabaseConnectRequest());
            AddConnector(Connector.Local, new LocalSourceConnectRequest());
            base.OnInitialized();
        }

        private void AddConnector(Connector connector, object initialAction)
        {
            _tasks.Add(new StartupTask(connector, initialAction));
        }

        private void OnApplicationStarted(ApplicationStartedAction action)
        {
            Submit();
        }

        protected override void OnParametersSet()
        {
            if (Started)
                return;

            Started = true;

            foreach (var task in _tasks)
            {
                Dispatcher.Dispatch(task.InitialAction);
            }
        }
    }
}
