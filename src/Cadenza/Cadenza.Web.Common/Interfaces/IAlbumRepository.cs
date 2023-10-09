namespace Cadenza.Web.Common.Interfaces;

public interface IAlbumRepository
{
    Task<AlbumDetailsVM> GetAlbum(int id);
    Task<List<AlbumTrackVM>> GetAlbumTracks(int albumId);
}
