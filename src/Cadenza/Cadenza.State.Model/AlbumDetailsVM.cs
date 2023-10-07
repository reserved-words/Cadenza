namespace Cadenza.State.Model;

public record AlbumDetailsVM : AlbumVM
{
    public int DiscCount { get; init; }
    public List<int> TrackCounts { get; init; }
    public IReadOnlyCollection<string> Tags { get; init; }
}