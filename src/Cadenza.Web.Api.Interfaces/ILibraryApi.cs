namespace Cadenza.Web.Api.Interfaces;

public interface ILibraryApi
{
    Task<AlbumVM> GetAlbum(int id);
    Task<ArtistVM> GetArtist(int id);
    Task<List<ArtistVM>> GetArtistsByGenre(string id);
    Task<List<ArtistVM>> GetArtistsByGrouping(int id);
    Task<AlbumFullVM> GetFullAlbum(int id);
    Task<ArtistFullVM> GetFullArtist(int id, bool includeAlbumsByOtherArtists);
    Task<TrackFullVM> GetFullTrack(int id);
    Task<List<TaggedItemVM>> GetTag(string id);
    Task<TrackDetailsVM> GetTrack(int id);
}
