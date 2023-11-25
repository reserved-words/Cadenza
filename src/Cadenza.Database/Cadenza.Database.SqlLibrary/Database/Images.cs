using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Images;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Images : IImages
{
    private readonly ISqlAccess _sql;

    public Images(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Images));
    }

    public async Task<GetAlbumArtworkResult> GetAlbumArtwork(int id)
    {
        return await _sql.QuerySingle<GetAlbumArtworkResult>(new { Id = id });
    }

    public async Task<GetArtistImageResult> GetArtistImage(int id)
    {
        return await _sql.QuerySingle<GetArtistImageResult>(new { Id = id });
    }
}
