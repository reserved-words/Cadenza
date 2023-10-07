namespace Cadenza.State.Actions;

public record FetchViewArtistRequest(int ArtistId);

public record FetchViewArtistResult(ArtistDetailsVM Artist, List<ArtistReleaseGroupVM> Releases);
