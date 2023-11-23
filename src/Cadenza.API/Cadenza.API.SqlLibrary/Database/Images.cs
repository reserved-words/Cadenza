namespace Cadenza.Database.SqlLibrary.Database;

internal class Images : IImages
{
    private readonly ISqlAccess _sql;

    public Images(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Images));
    }

    public async Task<AlbumArtwork> GetAlbumArtwork(int id)
    {
        return await _sql.QuerySingle<AlbumArtwork>(new { Id = id });
    }

    public async Task<ArtistImage> GetArtistImage(int id)
    {
        return await _sql.QuerySingle<ArtistImage>(new { Id = id });
    }
}
