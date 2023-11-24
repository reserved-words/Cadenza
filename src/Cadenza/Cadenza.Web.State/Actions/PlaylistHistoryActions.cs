namespace Cadenza.Web.State.Actions;

public record FetchPlaylistHistoryRequest(PlaylistId Playlist);

public record FetchPlaylistHistoryAlbumsRequest(); 
public record FetchPlaylistHistoryAlbumsResult(IReadOnlyCollection<RecentAlbumVM> Result);

public record FetchPlaylistHistoryTagsRequest(); 
public record FetchPlaylistHistoryTagsResult(IReadOnlyCollection<string> Result);
