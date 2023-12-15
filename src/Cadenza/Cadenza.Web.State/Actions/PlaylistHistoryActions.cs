using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchPlaylistHistoryRequest(PlaylistId Playlist);

public record FetchPlaylistHistoryAlbumsRequest(); 
public record FetchPlaylistHistoryAlbumsResult(IReadOnlyCollection<RecentAlbumVM> Result);
