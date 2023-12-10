using Cadenza.Common.Enums;
using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record CancelEditItemRequest();
public record RemoveEditItemRequest(LibraryItemType Type, int Id);
public record SaveEditItemRequest(LibraryItemType Type, int Id);

public record FetchEditArtistRequest(int ArtistId);
public record FetchEditArtistResult(ArtistDetailsVM Artist, IReadOnlyCollection<AlbumVM> Releases);

public record FetchEditAlbumRequest(int AlbumId);
public record FetchEditAlbumResult(AlbumDetailsVM Album, IReadOnlyCollection<AlbumTrackVM> Tracks);

public record FetchEditTrackRequest(int TrackId);
public record FetchEditTrackResult(TrackDetailsVM Track);