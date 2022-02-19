using Cadenza.Common;
using Cadenza.Core.Model;
using Cadenza.Library;

namespace Cadenza.Components.Tabs.Items
{
    public class AlbumTabBase : ComponentBase
    {
        [Inject]
        public IMergedAlbumRepository Repository { get; set; }

        [Parameter]
        public LibrarySource Source { get; set; }

        [Parameter]
        public string Id { get; set; }

        public AlbumInfo Album { get; set; }

        public List<Disc> Discs { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            await UpdateAlbum();
        }

        private async Task UpdateAlbum()
        {
            Album = await Repository.GetAlbum(Source, Id);

            var tracks = await Repository.GetTracks(Source, Id);

            Discs = tracks.GroupByDisc();

            StateHasChanged();
        }
    }
}
