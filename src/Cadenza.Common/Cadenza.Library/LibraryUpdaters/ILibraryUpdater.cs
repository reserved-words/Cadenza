namespace Cadenza.Library;

public interface ILibraryUpdater
{
    Task<bool> UpdateAlbum(AlbumInfo album);
    Task<bool> UpdateArtist(ArtistInfo artist);
    Task<bool> UpdateTrack(TrackInfo track);
}
