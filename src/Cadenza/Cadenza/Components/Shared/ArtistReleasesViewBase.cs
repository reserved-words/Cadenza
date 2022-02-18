using Cadenza.Core.Model;
using Cadenza.Library;
using Cadenza.Utilities;

namespace Cadenza.Components.Shared;

public class ArtistReleasesViewBase : ComponentBase
{
    [Parameter]
    public List<ArtistReleaseGroup> Model { get; set; } = new();
}