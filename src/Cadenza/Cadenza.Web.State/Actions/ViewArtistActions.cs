using Cadenza.Web.Model;

namespace Cadenza.Web.State.Actions;

public record FetchViewArtistRequest(int ArtistId);

public record FetchViewArtistResult(ArtistDetailsVM Artist, IReadOnlyCollection<ArtistReleaseGroupVM> Releases);
