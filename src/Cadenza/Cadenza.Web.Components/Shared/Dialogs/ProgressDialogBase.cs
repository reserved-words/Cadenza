using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class ProgressDialogBase : DialogBase
    {
        [Inject] public ILongRunningTaskService Service { get; set; }

        [Parameter] public Func<List<SubTask>> TaskFactory { get; set; }

        public bool Started => SubTasks.Values.Any(t => t.State != TaskState.None);
        public bool InProgress => SubTasks.Values.Any(t => t.State == TaskState.Running);
        public bool Ended => SubTasks.Values.All(t => t.State == TaskState.Completed || t.State == TaskState.Errored);
        public bool Errored => SubTasks.Values.Any(t => t.State == TaskState.Errored);

        public bool AtLeastOneTaskErrored => SubTasks.Any(t => t.Value.State == TaskState.Errored);

        public Dictionary<string, SubTaskProgress> SubTasks { get; set; } = new Dictionary<string, SubTaskProgress>();

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
               // OnClose();
            }
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
