namespace Cadenza.Local.API.Interfaces;

public interface IExternalSourceService
{
    Task AddArtist(Artist artist);
    Task AddAlbum(AlbumInfo album);
    Task AddTrack(TrackInfo track, AlbumTrackPosition position);
}