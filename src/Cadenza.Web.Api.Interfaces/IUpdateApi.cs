namespace Cadenza.Web.Api.Interfaces;

public interface IUpdateApi
{
    Task UpdateAlbum(int albumId, AlbumDetailsVM updatedAlbum, IReadOnlyCollection<AlbumTrackVM> updatedTracks, IReadOnlyCollection<int> removedTracks);
    Task UpdateArtist(int artistId, ArtistDetailsVM updatedArtist, IReadOnlyCollection<AlbumVM> updatedReleases);
    Task UpdateTrack(TrackDetailsVM updatedTrack);
}