using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Actions;

public record FetchPlaylistHistoryAlbumsRequest();
public record FetchPlaylistHistoryAlbumsResult(List<RecentAlbum> Result);

public record FetchPlaylistHistoryTagsRequest();
public record FetchPlaylistHistoryTagsResult(List<string> Result);
