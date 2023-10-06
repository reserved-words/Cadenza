using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Actions;

public record LogPlayedItemRequest(PlaylistId Playlist);
public record LogPlayedItemCompletedAction(PlaylistId Playlist);

public record FetchPlaylistHistoryAlbumsRequest();
public record FetchPlaylistHistoryAlbumsResult(List<RecentAlbum> Result);

public record FetchPlaylistHistoryTagsRequest();
public record FetchPlaylistHistoryTagsResult(List<string> Result);
