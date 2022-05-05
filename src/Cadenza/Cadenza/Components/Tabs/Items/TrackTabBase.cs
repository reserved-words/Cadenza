using Cadenza.Core.App;
using Cadenza.Library;

namespace Cadenza.Components.Tabs.Items
{
    public class TrackTabBase : ComponentBase
    {
        [Inject]
        public ITrackRepository Repository { get; set; }

        [Inject]
        public IItemPlayer Player { get; set; }

        [Inject]
        public IItemViewer Viewer { get; set; }

        [Parameter]
        public string Id { get; set; }

        public TrackFull Model { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateTrack();
        }

        protected async Task OnPlayTrack(Track track)
        {
            await Player.PlayTrack(track.Id);
        }

        protected async Task OnPlayAlbum(Album album)
        {
            await Player.PlayAlbum(album.Id);
        }

        protected async Task OnPlayArtist(Artist artist)
        {
            await Player.PlayArtist(artist.Id);
        }

        private async Task UpdateTrack()
        {
            Model = await Repository.GetTrack(Id);

            StateHasChanged();
        }

        protected async Task OnViewTrack()
        {
            await Viewer.ViewTrack(Model.Track);
        }

        protected async Task OnViewAlbum()
        {
            await Viewer.ViewAlbum(Model.Album);
        }

        protected async Task OnViewArtist()
        {
            await Viewer.ViewArtist(Model.Artist);
        }
    }
}
