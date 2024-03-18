namespace Cadenza.Web.Api.Interfaces;

public interface IPlayApi
{
    Task<PlaylistVM> PlayAll();
    Task<PlaylistVM> PlayAlbum(int id);
    Task<PlaylistVM> PlayArtist(int id);
    Task<PlaylistVM> PlayGenre(string genre);
    Task<PlaylistVM> PlayGrouping(string grouping);
    Task<PlaylistVM> PlayTag(string id);
    Task<PlaylistVM> PlayTrack(int id);
}
