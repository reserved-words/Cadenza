﻿using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class LibraryRepository : ILibraryRepository
{
    private readonly ILibraryMapper _mapper;
    private readonly ILibrary _library;

    public LibraryRepository(ILibraryMapper mapper, ILibrary library)
    {
        _mapper = mapper;
        _library = library;
    }

    public async Task<AlbumFullDTO> GetAlbumFull(int id)
    {
        var album = await _library.GetFullAlbum(id);
        var discs = await _library.GetAlbumDiscs(id);
        var tracks = await _library.GetAlbumTracks(id);

        var mappedAlbum = _mapper.MapAlbum(album);
        var mappedDiscs = _mapper.MapAlbumTracks(id, discs, tracks);

        mappedAlbum.Discs = mappedDiscs;

        return mappedAlbum;
    }

    public async Task<ArtistFullDTO> GetFullArtist(int id, bool includeAlbumsByOtherArtists)
    {
        var artist = await _library.GetFullArtist(id);
        var mappedArtist = _mapper.MapArtist(artist);

        var albums = await _library.GetArtistAlbums(id);
        var mappedAlbums = albums.Select(_mapper.MapAlbum).ToList();

        mappedArtist.Albums = mappedAlbums;

        if (includeAlbumsByOtherArtists)
        {
            var otherAlbums = await _library.GetAlbumsFeaturingArtist(id);
            var mappedOtherAlbums = otherAlbums.Select(_mapper.MapAlbum).ToList();

            mappedArtist.AlbumsFeaturingArtist = mappedOtherAlbums;
        }

        return mappedArtist;
    }

    public async Task<GenreDTO> GetArtistsByGenre(string grouping, string genre)
    {
        var artists = await _library.GetArtistsByGenre(genre, grouping);
        return _mapper.MapGenre(grouping, genre, artists);
    }

    public async Task<List<ArtistDTO>> GetArtistsByGrouping(string grouping)
    {
        var artists = await _library.GetArtistsByGrouping(grouping);
        return artists.Select(_mapper.MapArtist).ToList();
    }

    public async Task<List<TaggedItemDTO>> GetTaggedItems(string tag)
    {
        var items = await _library.GetTaggedItems(tag);
        return items.Select(_mapper.MapTaggedItem).ToList();
    }

    public async Task<TrackFullDTO> GetTrackFull (int id)
    {
        var track = await _library.GetFullTrack(id);
        return _mapper.MapTrack(track);
    }

    public async Task<TrackDetailsDTO> GetTrack(int id)
    {
        var track = await _library.GetTrack(id);
        return _mapper.MapTrack(track);
    }

    public async Task<List<string>> GetGroupings()
    {
        return await _library.GetGroupings();
    }
}
