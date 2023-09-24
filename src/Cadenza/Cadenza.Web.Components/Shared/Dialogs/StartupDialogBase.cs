using Cadenza.State.Actions;
using Cadenza.State.Store;
using Fluxor;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class StartupDialogBase : DialogBase
    {
        [Inject] public IDispatcher Dispatcher { get; set; }
        [Inject] public IState<DatabaseConnectionState> DatabaseConnectionState { get; set; }
        [Inject] public IState<LocalSourceConnectionState> LocalSourceConnectionState { get; set; }
        [Inject] public IState<LastFmConnectionState> LastFmConnectionState { get; set; }

        protected bool Started;

        private readonly List<StartupTask> _tasks = new();

        protected override void OnInitialized()
        {
            AddConnector(Connector.LastFm, new LastFmConnectRequest(), LastFmConnectionState);
            AddConnector(Connector.Database, new DatabaseConnectRequest(), DatabaseConnectionState);
            AddConnector(Connector.Local, new LocalSourceConnectRequest(), LocalSourceConnectionState);
            base.OnInitialized();
        }

        private void AddConnector<T>(Connector connector, object initialAction, IState<T> state)
        {
            _tasks.Add(new StartupTask(connector, initialAction));
            state.StateChanged += OnConnectorStateChanged;
        }

        private void OnConnectorStateChanged(object sender, EventArgs e)
        {
            OnSubTaskProgressed();
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

        private void OnSubTaskProgressed()
        {
            StateHasChanged();

            if (DatabaseConnectionState.Value.State == TaskState.Completed
                && LastFmConnectionState.Value.State == TaskState.Completed
                && LocalSourceConnectionState.Value.State == TaskState.Completed)
            {
                // Submit();
            }
        }
    }
}
