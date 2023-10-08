using Cadenza.Web.Model;

namespace Cadenza.Web.Components.Main.ViewBases;

public class TrackViewBase : FluxorComponent
{
    [Parameter] public TrackDetailsVM Model { get; set; }
}
