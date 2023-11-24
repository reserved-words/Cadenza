using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IHistory
{
    Task<List<GetRecentAlbumsResult>> GetRecentAlbums(int maxItems);
    Task<List<GetRecentTagsResult>> GetRecentTags(int maxItems);
}
