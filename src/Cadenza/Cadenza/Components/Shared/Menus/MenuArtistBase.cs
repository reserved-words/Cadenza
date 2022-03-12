using Cadenza.Components.Shared.Dialogs;
using Cadenza.Core.App;
using Cadenza.Core.Updates;
using Cadenza.Library;
using Cadenza.Update.LibraryUpdaters;

namespace Cadenza;

public class MenuArtistBase : ComponentBase
{
    [Inject]
    public IMergedArtistRepository Repository { get; set; }

    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public IItemPlayer Player { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public string Id { get; set; }

    public async Task OnEdit()
    {
        var artist = await Repository.GetArtist(Id);
        await DialogService.DisplayForm<EditArtist, ArtistInfo>(artist, "Edit Artist");
    }

    public async Task OnPlay()
    {
        await Player.PlayArtist(Id);
    }
}
