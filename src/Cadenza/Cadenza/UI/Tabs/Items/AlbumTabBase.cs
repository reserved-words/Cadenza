using Cadenza.Core.App;
using Cadenza.Core.Extensions;
using Cadenza.Core.Model;
using Cadenza.Library;

namespace Cadenza.UI.Tabs.Items
{

    public class AlbumTabBase : ComponentBase
    {
        [Inject]
        public IAlbumRepository Repository { get; set; }

        [Inject]
        public IItemPlayer Player { get; set; }

        [Inject]
        public IItemViewer Viewer { get; set; }

        [Parameter]
        public string Id { get; set; }

        public AlbumInfo Album { get; set; }

        public List<Disc> Discs { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            await UpdateAlbum();
        }

        protected async Task OnPlay(Album album)
        {
            await Player.PlayAlbum(album.Id);
        }

        private async Task UpdateAlbum()
        {
            Album = await Repository.GetAlbum(Id);

            var tracks = await Repository.GetTracks(Id);

            Discs = tracks.GroupByDisc();

            StateHasChanged();
        }

        protected async Task OnViewAlbum()
        {
            await Viewer.ViewAlbum(Album);
        }

        protected async Task ViewTrack(string id, string title)
        {
            await Viewer.ViewTrack(id, title);
        }

        protected async Task ViewArtist(string id, string name)
        {
            await Viewer.ViewArtist(id, name);
        }
    }
}
