using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Components.Main.ViewBases;

public class TrackViewBase : FluxorComponent
{
    [Parameter] public TrackDetailsVM Model { get; set; }
}
