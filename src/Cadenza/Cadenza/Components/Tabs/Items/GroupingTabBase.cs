using Cadenza.Core.App;
using Cadenza.Core.Updates;
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
            var grouping = Id.Parse<Grouping>();
            var artists = await Repository.GetArtistsByGrouping(grouping);
            _artistsByGenre = artists.ToGroupedDictionary(a => a.Genre);
            Genres = _artistsByGenre.Keys.ToList();
            StateHasChanged();
        }

        private Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs e)
        {
            if (e.Update.IsUpdated(ItemProperty.Grouping, out ItemPropertyUpdate groupingUpdate))
            {
                if (groupingUpdate.OriginalValue == Id)
                {
                    _artistsByGenre.RemoveWhere(a => a.Id == e.Update.Id);
                }
                else if (groupingUpdate.UpdatedValue == Id)
                {
                    _artistsByGenre.AddThenSort(e.Update.UpdatedItem, a => a.Genre, a => a.Name);
                }
            }

            if (e.Update.IsUpdated(ItemProperty.Genre, out ItemPropertyUpdate genreUpdate))
            {
                if (genreUpdate.OriginalValue == Id)
                {
                    Artists.RemoveWhere(a => a.Id == e.Update.Id);
                }
                else if (genreUpdate.UpdatedValue == Id)
                {
                    Artists.AddThenSort(e.Update.UpdatedItem, a => a.Name);
                }
            }

            StateHasChanged();

            return Task.CompletedTask;
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
