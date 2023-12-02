namespace Cadenza.Web.Api.Interfaces;

public interface IAlbumApi
{
    Task<AlbumDetailsVM> GetAlbum(int id);
    Task<AlbumTracksVM> GetAlbumTracks(int albumId);
}
