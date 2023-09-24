using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class ProgressDialogBase : DialogBase
    {
        [Inject] public ILongRunningTaskService Service { get; set; }

        [Parameter] public Func<List<SubTask>> TaskFactory { get; set; }

        public bool Started => State.Started();
        public bool InProgress => State.InProgress();
        public bool Ended => State.Ended();

        public bool AtLeastOneTaskErrored => SubTasks.Any(t => t.Value.State == TaskState.Errored);

        public string ProgressMessage { get; set; }
        public TaskState State { get; set; }

        public Dictionary<string, SubTaskProgress> SubTasks { get; set; } = new Dictionary<string, SubTaskProgress>();

        protected override void OnInitialized()
        {
            SubscribeToAction<TaskGroupProgressedAction>(OnTaskGroupProgressed);
            SubscribeToAction<SubTaskProgressedAction>(OnSubTaskProgressed);
            base.OnInitialized();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Started)
                return;

            await OnStart();
        }

        private void OnTaskGroupProgressed(TaskGroupProgressedAction action)
        {
            ProgressMessage = action.Message;
            State = action.State;
            StateHasChanged();
            if (State == TaskState.Completed)
            {
                OnClose();
            }
        }

        private void OnSubTaskProgressed(SubTaskProgressedAction action)
        {
            var task = SubTasks[action.Id];
            task.Message = action.Message;
            task.State = action.State;
            StateHasChanged();
        }

        protected async Task OnStart()
        {
            var tasks = TaskFactory();

            SubTasks = tasks
                .ToDictionary(t => t.Id, t => new SubTaskProgress
                {
                    Title = t.Title,
                    State = TaskState.None,
                    Message = ""
                });

            await Service.RunTasks(tasks);
        }

        protected void OnClose()
        {
            Submit();
        }
    }
}
