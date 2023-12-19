﻿using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class SearchRepository : ISearchRepository
{
    private readonly ISearch _search;
    private readonly ISearchMapper _mapper;

    public SearchRepository(ISearch search, ISearchMapper mapper)
    {
        _search = search;
        _mapper = mapper;
    }

    public async Task<List<SearchItemDTO>> GetAlbums()
    {
        var albums = await _search.GetAlbums();
        return albums.Select(_mapper.MapAlbum).ToList();
    }

    public async Task<List<SearchItemDTO>> GetArtists()
    {
        var albums = await _search.GetArtists();
        return albums.Select(_mapper.MapArtist).ToList();
    }

    public async Task<List<SearchItemDTO>> GetGenres()
    {
        var genres = await _search.GetGenres();
        var groupedGenres = genres.GroupBy(g => g.Genre);

        var list = new List<SearchItemDTO>();

        foreach (var group in groupedGenres)
        {
            var isUniqueGenre = group.Count() == 1;

            foreach (var genre in group)
            {
                list.Add(_mapper.MapGenre(genre, isUniqueGenre));
            }
        }

        return list;
    }

    public async Task<List<SearchItemDTO>> GetGroupings()
    {
        var albums = await _search.GetGroupings();
        return albums.Select(_mapper.MapGrouping).ToList();
    }

    public async Task<List<SearchItemDTO>> GetTags()
    {
        var albums = await _search.GetTags();
        return albums.Select(_mapper.MapTag).ToList();
    }

    public async Task<List<SearchItemDTO>> GetTracks()
    {
        var albums = await _search.GetTracks();
        return albums.Select(_mapper.MapTrack).ToList();
    }
}
