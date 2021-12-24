namespace Cadenza.Components.Shared.Dialogs
{
    public class SyncSource
    {
        public LibrarySource Source { get; set; }
        public string ProgressMessage { get; set; }
        public SyncState SyncState { get; set; } = SyncState.None;
    }

    public class StartupSyncDialogBase : DialogBase
    {
        [Inject]
        public IStartupSyncService Service { get; set; }

        public List<SyncSource> SyncSources { get; set; }

        public string ProgressMessage { get; set; }
        public SyncState SyncState { get; set; } = SyncState.None;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        protected override async Task OnInitializedAsync()
        {
            SyncSources = new List<SyncSource>();
            SyncSources.Add(new SyncSource { Source = LibrarySource.Local });
            SyncSources.Add(new SyncSource { Source = LibrarySource.Spotify });

            Service.ProgressChanged += Service_ProgressChanged;
            Service.SyncProgressChanged += Service_SyncProgressChanged;
        }

        private async Task Service_ProgressChanged(object sender, ProgressEventArgs e)
        {
            if (e.Completed)
            {
                Submit();
                return;
            }

            ProgressMessage = e.Message;

            SyncState = e.Completed
                ? SyncState.Completed
                : e.Cancelled
                ? SyncState.Cancelled
                : SyncState.Running;

            StateHasChanged();
        }

        private async Task Service_SyncProgressChanged(object sender, SyncProgressEventArgs e)
        {
            var syncSource = SyncSources.Single(s => s.Source == e.Source);

            syncSource.ProgressMessage = e.Message;

            syncSource.SyncState = e.Completed
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
