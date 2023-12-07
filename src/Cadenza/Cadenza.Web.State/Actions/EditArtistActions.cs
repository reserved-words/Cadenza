using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchEditArtistRequest(int ArtistId);

public record FetchEditArtistResult(ArtistDetailsVM Artist, IReadOnlyCollection<AlbumVM> Releases);
