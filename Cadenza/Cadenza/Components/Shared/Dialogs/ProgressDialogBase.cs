namespace Cadenza.Components.Shared.Dialogs
{

    public class ProgressDialogBase : DialogBase
    {
        [Inject]
        public ILongRunningTaskService Service { get; set; }

        [Parameter]
        public TaskGroup TaskGroup { get; set; }

        [Parameter]
        public string StartPromptText { get; set; }

        public bool Started => State.Started();
        public bool InProgress => State.InProgress();
        public bool Ended => State.Ended();

        public string ProgressMessage { get; set; }
        public TaskState State { get; set; }
        public Dictionary<string, SubTaskProgress> SubTasks { get; set; }

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        protected override void OnInitialized()
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
            State = e.State;
            StateHasChanged();
        }

        private async Task OnSubTaskProgressChanged(object sender, SubTaskProgressEventArgs e)
        {
            var task = SubTasks[e.Id];
            task.Message = e.Message;
            task.State = e.State;
            StateHasChanged();
        }

        protected async Task OnStart()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
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
