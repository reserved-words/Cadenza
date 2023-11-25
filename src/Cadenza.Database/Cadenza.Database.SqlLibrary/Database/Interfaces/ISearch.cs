using Cadenza.Database.SqlLibrary.Model.Search;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface ISearch
{
    Task<List<GetAlbumsResult>> GetAlbums();
    Task<List<GetArtistsResult>> GetArtists();
    Task<List<GetGenresResult>> GetGenres();
    Task<List<GetGroupingsResult>> GetGroupings();
    Task<List<GetTagsResult>> GetTags();
    Task<List<GetTracksResult>> GetTracks();
}
