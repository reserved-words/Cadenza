namespace Cadenza.Web.Common.ViewModel;

public record ArtistReleaseGroupVM(ReleaseTypeGroup Group, IReadOnlyCollection<AlbumVM> Albums);