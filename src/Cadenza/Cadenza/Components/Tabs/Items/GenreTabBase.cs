using Cadenza.Core.App;
using Cadenza.Library;

namespace Cadenza.Components.Tabs.Items
{
    public class GenreTabBase : ComponentBase
    {
        [Inject]
        public IMergedArtistRepository Repository { get; set; }

        [Inject]
        public IItemPlayer Player { get; set; }

        [Inject]
        public IItemViewer Viewer { get; set; }

        [Parameter]
        public string Id { get; set; }

        public List<Artist> Artists { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            await UpdateGenre();
        }

        protected async Task OnView()
        {
            await Viewer.ViewGenre(Id);
        }

        private async Task UpdateGenre()
        {
            Artists = await Repository.GetArtistsByGenre(Id);

            StateHasChanged();
        }

        protected async Task OnPlayArtist(Artist artist)
        {
            await Player.PlayArtist(artist.Id);
        }

        protected async Task OnViewArtist(Artist artist)
        {
            await Viewer.ViewArtist(artist);
        }
    }
}
