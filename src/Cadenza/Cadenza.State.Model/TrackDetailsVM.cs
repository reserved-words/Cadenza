namespace Cadenza.State.Model;

public record TrackDetailsVM : TrackVM
{
    public string Year { get; init; }
    public string Lyrics { get; init; }
    public IReadOnlyCollection<string> Tags { get; init; }
}
