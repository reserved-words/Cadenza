namespace Cadenza.Web.Common.Model;

public class PlaylistDefinition
{
    public PlaylistId Id { get; set; }
    public List<int> Tracks { get; set; }
    public int StartIndex { get; set; }
}
