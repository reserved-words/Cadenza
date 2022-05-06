using Cadenza.Core.App;
using Cadenza.Core.Updates;
using Cadenza.Library;

namespace Cadenza.UI.Tabs.Items
{
    public class GenreTabBase : ComponentBase
    {
        [Inject]
        public IArtistRepository Repository { get; set; }

        [Inject]
        public IUpdatesConsumer Updates { get; set; }

        [Parameter]
        public string Id { get; set; }

        public List<Artist> Artists { get; set; } = new();

        protected override void OnInitialized()
        {
            Updates.ArtistUpdated += OnArtistUpdated;
        }

        private Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
        {
            if (!e.Update.IsUpdated(ItemProperty.Genre, out ItemPropertyUpdate genreUpdate))
                return Task.CompletedTask;

            if (genreUpdate.OriginalValue == Id)
            {
                Artists.RemoveWhere(a => a.Id == e.Update.Id);
            }

            if (genreUpdate.UpdatedValue == Id)
            {
                Artists = Artists.AddThenSort(e.Update.UpdatedItem, a => a.Name);
            }

            StateHasChanged();

            return Task.CompletedTask;
        }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateGenre();
        }

        private async Task UpdateGenre()
        {
            Artists = await Repository.GetArtistsByGenre(Id);

            StateHasChanged();
        }
    }
}
