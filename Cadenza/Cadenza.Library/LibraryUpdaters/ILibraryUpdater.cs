namespace Cadenza.Library;

public interface ILibraryUpdater
{
    Task<bool> UpdateAlbum(AlbumUpdate album);
    Task<bool> UpdateArtist(ArtistUpdate artist);
    Task<bool> UpdateTrack(TrackUpdate track);
}
