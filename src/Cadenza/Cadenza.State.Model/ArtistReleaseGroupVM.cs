namespace Cadenza.State.Model;

public record ArtistReleaseGroupVM(ReleaseTypeGroup Group, IReadOnlyCollection<AlbumVM> Albums);