using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchViewArtistRequest(int ArtistId);

public record FetchViewArtistResult(ArtistDetails Artist, List<ArtistReleaseGroup> Releases);
