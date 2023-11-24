using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IHistoryMapper
{
    RecentAlbumDTO MapRecentAlbum(GetRecentAlbumsResult data);
}
