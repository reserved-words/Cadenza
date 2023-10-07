namespace Cadenza.State.Actions;

public record FetchViewAlbumRequest(int AlbumId);

public record FetchViewAlbumResult(AlbumDetailsVM Album, IReadOnlyCollection<DiscVM> Discs);
