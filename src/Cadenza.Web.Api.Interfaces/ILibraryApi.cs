namespace Cadenza.Web.Api.Interfaces;

public interface ILibraryApi
{
    Task<GenreFullVM> GetArtistsByGenre(string genre);
    Task<List<ArtistVM>> GetArtistsByGrouping(string grouping);
    Task<AlbumFullVM> GetFullAlbum(int id);
    Task<ArtistFullVM> GetFullArtist(int id, bool includeAlbumsByOtherArtists);
    Task<TrackFullVM> GetFullTrack(int id);
    Task<List<string>> GetGroupings();
    Task<List<TaggedItemVM>> GetTag(string id);
    Task<TrackDetailsVM> GetTrack(int id);
}
