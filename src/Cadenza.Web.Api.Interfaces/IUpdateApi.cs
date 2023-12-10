namespace Cadenza.Web.Api.Interfaces;

public interface IUpdateApi
{
    Task UpdateAlbum(int albumId, AlbumDetailsVM updatedAlbum, IReadOnlyCollection<AlbumTrackVM> updatedTracks, IReadOnlyCollection<int> removedTracks);
    Task UpdateArtist(ArtistDetailsVM updatedArtist);
    Task UpdateTrack(TrackDetailsVM updatedTrack);
}