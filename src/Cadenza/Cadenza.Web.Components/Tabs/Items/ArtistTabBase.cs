﻿
using Cadenza.Web.Common.Extensions;
using Cadenza.Web.Common.Model;

namespace Cadenza.Web.Components.Tabs.Items
{
    public class ArtistTabBase : ComponentBase
    {
        [Inject]
        public IArtistRepository Repository { get; set; }

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

            var albums = await Repository.GetAlbums(Id);

            Releases = albums.GroupByReleaseType();

            StateHasChanged();
        }
    }
}
