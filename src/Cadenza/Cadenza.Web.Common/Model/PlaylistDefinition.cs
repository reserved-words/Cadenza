using Cadenza.Domain.Model;

namespace Cadenza.Web.Common.Model;

public class PlaylistDefinition
{
    public PlaylistId Id { get; set; }
    public List<PlayTrack> Tracks { get; set; }
}
