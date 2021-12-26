namespace Cadenza.Components.Shared.Dialogs
{
    public class SubTaskProgress
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public TaskState State { get; set; }
    }

    public class ProgressDialogBase : DialogBase
    {
        [Inject]
        public ILongRunningTaskService Service { get; set; }

        [Parameter]
        public TaskGroup TaskGroup { get; set; }

        public bool Running { get; set; }
        public string ProgressMessage { get; set; }
        public TaskState State { get; set; }
        public Dictionary<string, SubTaskProgress> SubTasks { get; set; }

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        protected override async Task OnInitializedAsync()
        {
            Service.TaskGroupProgressChanged += OnTaskGroupProgressChanged;
            Service.SubTaskProgressChanged += OnSubTaskProgressChanged;
        }

        protected override void OnParametersSet()
        {
            SubTasks = TaskGroup.Tasks
                .ToDictionary(t => t.Id, t => new SubTaskProgress 
                { 
                    Title = t.Title, 
                    State = TaskState.None, 
                    Message = "" 
                });
        }

        private async Task OnTaskGroupProgressChanged(object sender, TaskGroupProgressEventArgs e)
        {
            ProgressMessage = e.Message;

            // TODO: Errored

            State = e.Completed
                ? TaskState.Completed
                : e.Cancelled
                ? TaskState.Cancelled
                : TaskState.Running;

            StateHasChanged();
        }

        private async Task OnSubTaskProgressChanged(object sender, SubTaskProgressEventArgs e)
        {
            var task = SubTasks[e.Id];

            task.Message = e.Message;

            // TODO: Errored

            task.State = e.Completed
                ? TaskState.Completed
                : e.Cancelled
                ? TaskState.Cancelled
                : TaskState.Running;

            StateHasChanged();
        }

        protected async Task OnStart()
        {
            // also need a way to start straight away - parameter

            Running = true;

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            State = TaskState.Running;
            await Service.RunTasks(TaskGroup, _cancellationToken);
        }

        protected async Task OnCancel()
        {
            State = TaskState.Cancelling;
            _cancellationTokenSource.Cancel();
        }

        protected async Task OnClose()
        {
            Submit();
        }
    }
}
