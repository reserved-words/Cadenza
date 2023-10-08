using Cadenza.Web.Model;
using Cadenza.Web.State.Store;

namespace Cadenza.Web.Components.Tabs.Library;

public class GenreTabBase : FluxorComponent
{
    [Inject] public IState<ViewGenreState> ViewGenreState { get; set; }

    public bool Loading => ViewGenreState.Value.IsLoading;
    public string Genre => ViewGenreState.Value.Genre;
    public IReadOnlyCollection<ArtistVM> Artists => ViewGenreState.Value.Artists;
}
