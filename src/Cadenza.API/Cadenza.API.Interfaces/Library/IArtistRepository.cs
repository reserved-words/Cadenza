﻿namespace Cadenza.API.Interfaces.Library;

public interface IArtistRepository
{
    Task<List<AlbumDTO>> GetAlbums(int artistId);
    Task<List<AlbumDTO>> GetAlbumsFeaturingArtist(int artistId);
    Task<List<ArtistDTO>> GetArtistsByGenre(string id);
    Task<List<ArtistDTO>> GetArtistsByGrouping(int id);
}
