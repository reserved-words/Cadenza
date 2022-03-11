using Cadenza.Core.App;
using Cadenza.Library;

namespace Cadenza.Components.Tabs.Items
{
    public class GroupingTabBase : ComponentBase
    {
        [Inject]
        public IMergedArtistRepository Repository { get; set; }

        [Inject]
        public IItemPlayer Player { get; set; }

        [Inject]
        public IItemViewer Viewer { get; set; }

        [Parameter]
        public string Id { get; set; }

        public Grouping Grouping => Id.Parse<Grouping>();

        public List<string> Genres { get; set; } = new();

        public List<Artist> Artists => _artistsByGenre[SelectedGenre];

        public string SelectedGenre { get; set; }

        private Dictionary<string, List<Artist>> _artistsByGenre = new();

        protected override async Task OnParametersSetAsync()
        {
            await UpdateGrouping();
        }

        private async Task UpdateGrouping()
        {
            var grouping = Id.Parse<Grouping>();
            var artists = await Repository.GetArtistsByGrouping(grouping);

            _artistsByGenre = artists.GroupBy(a => a.Genre)
                .ToDictionary(g => g.Key, g => g.ToList());

            Genres = _artistsByGenre.Keys.ToList();

            StateHasChanged();
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

        protected async Task OnViewGenre(string id)
        {
            SelectedGenre = id;
            StateHasChanged();
        }

        protected async Task OnViewSelectedGenre()
        {
            await Viewer.ViewGenre(SelectedGenre);
        }
    }
}
