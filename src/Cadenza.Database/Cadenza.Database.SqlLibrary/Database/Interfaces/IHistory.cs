using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IHistory
{
    Task<List<GetNewScrobblesResult>> GetNewScrobbles();
    Task<List<GetRecentAlbumsResult>> GetRecentAlbums(int maxItems);
    Task<List<GetRecentTagsResult>> GetRecentTags(int maxItems);
    Task InsertScrobble(InsertScrobbleParameter parameters);
    Task MarkScrobbled(int scrobbleId);
}
