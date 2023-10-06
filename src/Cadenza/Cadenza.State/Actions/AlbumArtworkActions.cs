using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchAlbumArtworkRequest(Album Album);
public record FetchAlbumArtworkResultAction(int AlbumId, string Result);