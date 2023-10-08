using Cadenza.Web.Model;

namespace Cadenza.Web.Components.Player;

public class CurrentTrackBase : FluxorComponent
{
    [Parameter] public bool Empty { get; set; }
    [Parameter] public bool Loading { get; set; }
    [Parameter] public TrackFullVM Model { get; set; }
}
