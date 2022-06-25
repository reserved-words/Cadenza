using Cadenza.Library;

namespace Cadenza.UI.Tabs.Items
{
    public class TrackTabBase : ComponentBase
    {
        [Inject]
        public ITrackRepository Repository { get; set; }

        [Parameter]
        public string Id { get; set; }

        public TrackFull Model { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            await UpdateTrack();
        }

        private async Task UpdateTrack()
        {
            Model = await Repository.GetTrack(Id);

            StateHasChanged();
        }
    }
}
