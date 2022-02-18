using Cadenza.Common;

namespace Cadenza.Components.Shared;

public class ArtistViewBase : ComponentBase
{
    [Inject]
    public IPlaylistPlayer PlaylistPlayer { get; set; }

    [Parameter]
    public ArtistInfo Model { get; set; }

    [Parameter]
    public bool HeaderOnly { get; set; }

    protected async Task OnPlayArtist()
    {
        await PlaylistPlayer.PlayArtist(Model.Id);
    }

    public List<LinkViewModel> ArtistLinks => Model.Links();

    public string Location => Model.Location();
}