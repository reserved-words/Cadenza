namespace Cadenza.State.Model;

public record DiscVM(int DiscNo, IReadOnlyCollection<AlbumTrackVM> Tracks);