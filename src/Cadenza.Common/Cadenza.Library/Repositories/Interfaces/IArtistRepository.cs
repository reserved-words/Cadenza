﻿namespace Cadenza.Library;

public interface IArtistRepository
{
    Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit);
    Task<ListResponse<Artist>> GetAllArtists(int page, int limit);
    Task<ListResponse<Artist>> GetTrackArtists(int page, int limit);
    Task<ArtistInfo> GetArtist(string id);
    Task<ListResponse<Album>> GetAlbums(string artistId, int page, int limit);
    Task<ListResponse<Artist>> GetArtistsByGenre(string id, int page, int limit);
    Task<ListResponse<Artist>> GetArtistsByGrouping(Grouping id, int page, int limit);
}