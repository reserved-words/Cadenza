using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Components.Main.ViewBases;

public class ArtistViewBase : FluxorComponent
{
    [Parameter] public ArtistDetailsVM Model { get; set; }
}
