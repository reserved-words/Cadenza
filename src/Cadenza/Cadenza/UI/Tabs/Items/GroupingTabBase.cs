using Cadenza.Core.App;
using Cadenza.Core.Updates;
using Cadenza.Library;

namespace Cadenza.UI.Tabs.Items
{
    public class GroupingTabBase : ComponentBase
    {
        [Inject]
        public IArtistRepository Repository { get; set; }

        [Inject]
        public IItemPlayer Player { get; set; }

        [Inject]
        public IItemViewer Viewer { get; set; }

        [Inject]
        public IUpdatesConsumer Updates { get; set; }

        [Parameter]
        public string Id { get; set; }

        public Grouping Grouping => Id.Parse<Grouping>();

        public List<string> Genres { get; set; } = new();

        public List<Artist> Artists => _artistsByGenre[SelectedGenre];

        public string SelectedGenre { get; set; }

        private Dictionary<string, List<Artist>> _artistsByGenre = new();

        protected override void OnInitialized()
        {
            Updates.ArtistUpdated += OnArtistUpdated;
        }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateGrouping();
        }

        private async Task UpdateGrouping()
        {
            var currentGenre = SelectedGenre;

            var artists = await Repository.GetArtistsByGrouping(Id.Parse<Grouping>());
            _artistsByGenre = artists.ToGroupedDictionary(a => a.Genre ?? "None");
            Genres = _artistsByGenre.Keys.ToList();

            if (currentGenre != null && Genres.Contains(currentGenre))
            {
                SelectedGenre = currentGenre;
            }

            StateHasChanged();
        }

        private async Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
        {
            var isGroupingUpdated = e.Update.IsUpdated(ItemProperty.Grouping);
            var isGenreUpdated = e.Update.IsUpdated(ItemProperty.Genre);

            if (!isGroupingUpdated && !isGenreUpdated)
                return;

            await UpdateGrouping();
        }

        protected async Task OnView()
        {
            await Viewer.ViewGrouping(Grouping);
        }

        protected async Task OnPlayArtist(Artist artist)
        {
            await Player.PlayArtist(artist.Id);
        }

        protected async Task OnViewArtist(Artist artist)
        {
            await Viewer.ViewArtist(artist);
        }

        protected async Task OnPlayGenre(string id)
        {
            await Player.PlayGenre(id);
        }

        protected Task OnViewGenre(string id)
        {
            SelectedGenre = id;
            StateHasChanged();
            return Task.CompletedTask;
        }

        protected async Task OnViewSelectedGenre()
        {
            await Viewer.ViewGenre(SelectedGenre);
        }
    }
}
