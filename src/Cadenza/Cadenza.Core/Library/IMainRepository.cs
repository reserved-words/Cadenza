namespace Cadenza.Core;

public interface IMainRepository
{
    Task Clear();
    Task AddArtists(List<ArtistInfo> artists);
    Task AddAlbums(List<AlbumInfo> albums);
    Task AddTracks(LibrarySource source, List<BasicTrack> tracks);
    Task<IEnumerable<SearchableItem>> GetSearchableItems();
}