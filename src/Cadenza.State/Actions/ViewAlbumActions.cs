using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchViewAlbumRequest(int AlbumId);

public record FetchViewAlbumResult(AlbumDetails Album, List<Disc> Discs);
