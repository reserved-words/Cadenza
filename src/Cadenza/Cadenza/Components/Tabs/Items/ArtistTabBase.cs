using Cadenza.Common;
using Cadenza.Core.Model;
using Cadenza.Library;

namespace Cadenza.Components.Tabs.Items
{
    public class ArtistTabBase : ComponentBase
    {
        [Inject]
        public IMergedArtistRepository Repository { get; set; }

        [Parameter]
        public string Id { get; set; }

        public ArtistInfo Artist { get; set; }

        public List<ArtistReleaseGroup> Releases { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            await UpdateArtist();
        }

        private async Task UpdateArtist()
        {
            Artist = await Repository.GetArtist(Id);

            var albums = await Repository.GetArtistAlbums(Id);

            Releases = albums.GroupByReleaseType();

            StateHasChanged();
        }
    }
}
