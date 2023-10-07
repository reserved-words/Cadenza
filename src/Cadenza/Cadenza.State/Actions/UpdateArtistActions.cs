namespace Cadenza.State.Actions;

public record ArtistUpdateRequest(ArtistDetailsVM OriginalArtist, ArtistDetailsVM UpdatedArtist);
public record ArtistUpdatedAction(ArtistDetailsVM UpdatedArtist);
public record ArtistUpdateFailedAction(int ArtistId);