using Cadenza.Library;

namespace Cadenza.Components.Tabs.Items
{
    public class TrackTabBase : ComponentBase
    {
        [Inject]
        public IMergedTrackRepository Repository { get; set; }

        [Inject]
        public IItemPlayer Player { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public LibrarySource Source { get; set; }

        public TrackFull Model { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateTrack();
        }

        protected async Task OnPlayTrack(Track track)
        {
            await Player.PlayTrack(track.Source, track.Id);
        }

        protected async Task OnPlayAlbum(Album album)
        {
            await Player.PlayAlbum(album.Source, album.Id);
        }

        protected async Task OnPlayArtist(Artist artist)
        {
            await Player.PlayArtist(artist.Id);
        }

        private async Task UpdateTrack()
        {
            Model = await Repository.GetTrack(Source, Id);

            StateHasChanged();
        }
    }
}
