namespace Cadenza.Components.Shared.Dialogs
{
    public class StartupSyncDialogBase : DialogBase
    {
        [Inject]
        public IStartupSyncService Service { get; set; }

        public string ProgressMessage { get; set; }
        public SyncState SyncState { get; set; } = SyncState.None;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        protected override async Task OnInitializedAsync()
        {
            Service.ProgressChanged += Service_ProgressChanged;
        }

        private async Task Service_ProgressChanged(object sender, ProgressEventArgs e)
        {
            ProgressMessage = e.Message;

            SyncState = e.Completed
                ? SyncState.Completed
                : e.Cancelled
                ? SyncState.Cancelled
                : SyncState.Running;

            StateHasChanged();
        }

        protected override async Task OnParametersSetAsync()
        {
            await StartSync();
        }

        protected async Task StartSync()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            SyncState = SyncState.Running;
            await Service.SyncLibrary(_cancellationToken);
        }

        protected async Task OnCancel()
        {
            SyncState = SyncState.Cancelling;
            _cancellationTokenSource.Cancel();
        }

        protected async Task OnClose()
        {
            Submit();
        }
    }
}
