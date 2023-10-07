namespace Cadenza.State.Actions;

public record FetchViewTrackRequest(int TrackId);

public record FetchViewTrackResult(TrackFullVM Track);
