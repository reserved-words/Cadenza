using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchViewAlbumRequest(int AlbumId);

public record FetchViewAlbumResult(AlbumInfo Album, List<Disc> Discs);
