using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record ArtistUpdateRequest(int ArtistId, ArtistDetailsVM UpdatedArtist, IReadOnlyCollection<AlbumVM> UpdatedArtistReleases);
public record ArtistUpdatedAction(int ArtistId, ArtistDetailsVM UpdatedArtist);
public record ArtistReleasesUpdatedAction(int ArtistId, IReadOnlyCollection<AlbumVM> UpdatedArtistReleases);
public record ArtistUpdateFailedAction(int ArtistId);