using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Features.Player.Components;

public class CurrentTrackBase : FluxorComponent
{
    [Parameter] public bool Empty { get; set; }
    [Parameter] public bool Loading { get; set; }
    [Parameter] public TrackFullVM Model { get; set; }
}
