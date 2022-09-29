using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Track;
using IDialogService = Cadenza.Web.Components.Interfaces.IDialogService;

namespace Cadenza.Web.Components.Shared.Menus;

public class MenuLyricsBase : ComponentBase
{
    [Inject]
    public ITrackRepository Repository { get; set; }

    [Inject]
    public IDialogService DialogService { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter]
    public string Style { get; set; } = "";

    [Parameter]
    public Size Size { get; set; } = Size.Large;

    [Parameter]
    public LibrarySource Source { get; set; }

    [Parameter]
    public string Id { get; set; }

    public async Task OnEdit()
    {
        var track = await Repository.GetTrack(Id);
        await DialogService.DisplayForm<EditLyrics, TrackInfo>(track.Track, "Edit Lyrics", false);
    }
}
