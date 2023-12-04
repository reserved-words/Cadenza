using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchEditableAlbumTracksRequest(int AlbumId);
public record FetchEditableAlbumTracksResultAction(AlbumTracksVM Result);