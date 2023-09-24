using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class StartupDialogBase : DialogBase
    {
        [Inject] public ILongRunningTaskService Service { get; set; }

        [Parameter] public List<StartupTask> Tasks { get; set; }

        public bool InProgress => SubTasks.Values.Any(t => t.State == TaskState.Running);
        public bool Ended => SubTasks.Values.All(t => t.State == TaskState.Completed || t.State == TaskState.Errored);
        public bool Errored => SubTasks.Values.Any(t => t.State == TaskState.Errored);

        public bool AtLeastOneTaskErrored => SubTasks.Any(t => t.Value.State == TaskState.Errored);

        public Dictionary<Connector, StartupTaskProgress> SubTasks { get; set; } = new Dictionary<Connector, StartupTaskProgress>();

        protected bool Started;

        protected override void OnInitialized()
        {
            SubscribeToAction<StartupTaskProgressAction>(OnSubTaskProgressed);
            base.OnInitialized();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Started)
                return;

            await OnStart();
        }

        private void OnSubTaskProgressed(StartupTaskProgressAction action)
        {
            var task = SubTasks[action.Connector];
            task.Message = action.Message;
            task.State = action.State;
            StateHasChanged();

            if (Ended && !Errored)
            {
               // OnClose();
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
