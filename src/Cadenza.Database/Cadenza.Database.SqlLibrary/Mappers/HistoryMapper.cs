using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class HistoryMapper : IHistoryMapper
{

    public RecentAlbumDTO MapRecentAlbum(GetRecentAlbumsResult data)
    {
        return new RecentAlbumDTO
        {
            Id = data.AlbumId,
            Title = data.AlbumTitle,
            ArtistName = data.ArtistName
        };
    }
}
