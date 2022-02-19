using Cadenza.Library;

namespace Cadenza.Components.Tabs.Items
{
    public class TrackTabBase : ComponentBase
    {
        [Inject]
        public IMergedTrackRepository Repository { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public LibrarySource Source { get; set; }

        public TrackFull Model { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateTrack();
        }

        private async Task UpdateTrack()
        {
            Model = await Repository.GetTrack(Source, Id);

            StateHasChanged();
        }
    }
}
