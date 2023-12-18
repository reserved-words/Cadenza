using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchViewAlbumRequest(int AlbumId);

public record FetchViewAlbumResult(AlbumFullVM Album);
