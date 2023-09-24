using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class StartupDialogBase : DialogBase
    {
        [Inject] public ILongRunningTaskService Service { get; set; }

        [Parameter] public List<StartupTask> Tasks { get; set; }

        public bool Started => SubTasks.Values.Any(t => t.State != TaskState.None);
        public bool InProgress => SubTasks.Values.Any(t => t.State == TaskState.Running);
        public bool Ended => SubTasks.Values.All(t => t.State == TaskState.Completed || t.State == TaskState.Errored);
        public bool Errored => SubTasks.Values.Any(t => t.State == TaskState.Errored);

        public bool AtLeastOneTaskErrored => SubTasks.Any(t => t.Value.State == TaskState.Errored);

        public Dictionary<string, StartupTaskProgress> SubTasks { get; set; } = new Dictionary<string, StartupTaskProgress>();

        protected override void OnInitialized()
        {
            SubscribeToAction<SubTaskProgressedAction>(OnSubTaskProgressed);
            base.OnInitialized();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Started)
                return;

            await OnStart();
        }

        private void OnSubTaskProgressed(SubTaskProgressedAction action)
        {
            var task = SubTasks[action.Id];
            task.Message = action.Message;
            task.State = action.State;
            StateHasChanged();

            if (Ended && !Errored)
            {
                OnClose();
            }
        }

        protected async Task OnStart()
        {
            SubTasks = Tasks
                .ToDictionary(t => t.Id, t => new StartupTaskProgress
                {
                    Title = t.Title,
                    State = TaskState.None,
                    Message = ""
                });

            await Service.RunTasks(Tasks);
        }

        protected void OnClose()
        {
            Submit();
        }
    }
}
