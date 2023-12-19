namespace Cadenza.Web.Api.Interfaces;

public interface ILibraryApi
{
    Task<AlbumVM> GetAlbum(int id);
    Task<ArtistVM> GetArtist(int id);
    Task<GenreFullVM> GetGenre(string genre);
    Task<List<ArtistVM>> GetArtistsByGrouping(string grouping);
    Task<AlbumFullVM> GetFullAlbum(int id);
    Task<ArtistFullVM> GetFullArtist(int id, bool includeAlbumsByOtherArtists);
    Task<TrackFullVM> GetFullTrack(int id);
    Task<List<TaggedItemVM>> GetTag(string id);
    Task<TrackDetailsVM> GetTrack(int id);
}
