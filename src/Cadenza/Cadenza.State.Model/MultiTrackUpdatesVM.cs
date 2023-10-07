namespace Cadenza.State.Model;

public record MultiTrackUpdatesVM(IReadOnlyCollection<string> TrackIds, IReadOnlyCollection<PropertyUpdateVM> Updates);