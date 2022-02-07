﻿namespace Cadenza.Library;

public interface ISourceSearchRepository
{
    public LibrarySource Source { get; }
    Task<ListResponse<SearchableItem>> GetSearchAlbums(int page, int limit);
    Task<ListResponse<SearchableItem>> GetSearchArtists(int page, int limit);
    Task<ListResponse<SearchableItem>> GetSearchPlaylists(int page, int limit);
    Task<ListResponse<SearchableItem>> GetSearchTracks(int page, int limit);

}