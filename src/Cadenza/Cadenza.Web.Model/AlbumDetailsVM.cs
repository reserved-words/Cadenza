namespace Cadenza.Web.Model;

public record AlbumDetailsVM : AlbumVM
{
    public int DiscCount { get; init; }
    public IReadOnlyDictionary<int, int> DiscTrackCounts { get; init; }
    public IReadOnlyCollection<string> Tags { get; init; }
}