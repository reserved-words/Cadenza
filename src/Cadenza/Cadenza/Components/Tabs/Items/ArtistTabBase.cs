using Cadenza.Common;
using Cadenza.Core.Model;
using Cadenza.Library;

namespace Cadenza.Components.Tabs.Items
{
    public class ArtistTabBase : ComponentBase
    {
        [Inject]
        public IMergedArtistRepository Repository { get; set; }

        [Inject]
        public IItemPlayer Player { get; set; }

        [Inject]
        public IItemViewer Viewer { get; set; }

        [Parameter]
        public string Id { get; set; }

        public ArtistInfo Artist { get; set; }

        public List<ArtistReleaseGroup> Releases { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            await UpdateArtist();
        }

        protected async Task OnPlayArtist(Artist artist)
        {
            await Player.PlayArtist(artist.Id);
        }

        protected async Task OnPlayAlbum(Album album)
        {
            await Player.PlayAlbum(album.Source, album.Id);
        }

        private async Task UpdateArtist()
        {
            Artist = await Repository.GetArtist(Id);

            var albums = await Repository.GetArtistAlbums(Id);

            Releases = albums.GroupByReleaseType();

            StateHasChanged();
        }

        protected async Task OnViewArtist()
        {
            await Viewer.ViewArtist(Artist);
        }
    }
}
