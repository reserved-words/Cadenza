using Cadenza.State.Store;
using Cadenza.Web.Common.Interfaces.Store;
using Fluxor;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class StartupDialogBase : DialogBase
    {
        [Inject] public ILongRunningTaskService Service { get; set; }
        [Inject] public IState<DatabaseConnectionState> DatabaseConnectionState { get; set; }
        [Inject] public IState<LocalSourceConnectionState> LocalSourceConnectionState { get; set; }
        [Inject] public IState<LastFmConnectionState> LastFmConnectionState { get; set; }

        [Parameter] public List<StartupTask> Tasks { get; set; }

        public bool InProgress => SubTasks.Values.Any(t => t.State == TaskState.Running);
        public bool Ended => SubTasks.Values.All(t => t.State == TaskState.Completed || t.State == TaskState.Errored);
        public bool Errored => SubTasks.Values.Any(t => t.State == TaskState.Errored);

        public bool AtLeastOneTaskErrored => SubTasks.Any(t => t.Value.State == TaskState.Errored);

        public Dictionary<Connector, StartupTaskProgress> SubTasks { get; set; } = new Dictionary<Connector, StartupTaskProgress>();

        protected bool Started;

        protected override void OnInitialized()
        {
            DatabaseConnectionState.StateChanged += DatabaseConnectionState_StateChanged;
            LastFmConnectionState.StateChanged += LastFmConnectionState_StateChanged;
            LocalSourceConnectionState.StateChanged += LocalSourceConnectionState_StateChanged;

            base.OnInitialized();
        }

        private void DatabaseConnectionState_StateChanged(object sender, EventArgs e)
        {
            OnSubTaskProgressed(Connector.Database, DatabaseConnectionState.Value.State, DatabaseConnectionState.Value.Message);
        }

        private void LastFmConnectionState_StateChanged(object sender, EventArgs e)
        {
            OnSubTaskProgressed(Connector.LastFm, LastFmConnectionState.Value.State, LastFmConnectionState.Value.Message);
        }

        private void LocalSourceConnectionState_StateChanged(object sender, EventArgs e)
        {
            OnSubTaskProgressed(Connector.Local, LocalSourceConnectionState.Value.State, LocalSourceConnectionState.Value.Message);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Started)
                return;

            await OnStart();
        }

        private void OnSubTaskProgressed(Connector connector, TaskState state, string message)
        {
            var task = SubTasks[connector];
            task.Message = message;
            task.State = state;
            StateHasChanged();

            if (Ended && !Errored)
            {
                OnClose();
            }
        }

        protected async Task OnStart()
        {
            SubTasks = Tasks
                .ToDictionary(t => t.Connector, t => new StartupTaskProgress
                {
                    Title = t.Title,
                    State = TaskState.Running,
                    Message = ""
                });

            Started = true;

            await Service.RunTasks(Tasks);
        }

        protected void OnClose()
        {
            Submit();
        }
    }
}
