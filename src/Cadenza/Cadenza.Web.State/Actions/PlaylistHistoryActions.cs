using Cadenza.Web.Model;

namespace Cadenza.Web.State.Actions;

public record LogPlayedItemRequest(PlaylistId Playlist);
public record LogPlayedItemCompletedAction(PlaylistId Playlist);

public record FetchPlaylistHistoryAlbumsRequest();
public record FetchPlaylistHistoryAlbumsResult(IReadOnlyCollection<RecentAlbumVM> Result);

public record FetchPlaylistHistoryTagsRequest();
public record FetchPlaylistHistoryTagsResult(IReadOnlyCollection<string> Result);
