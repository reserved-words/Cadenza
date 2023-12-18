using Cadenza.Database.SqlLibrary.Database.Interfaces;
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

    public async Task<AlbumDetailsDTO> GetAlbum(int id)
    {
        var album = await _library.GetAlbum(id);
        return _mapper.MapAlbum(album);
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

    public async Task<List<AlbumDTO>> GetAlbumsFeaturingArtist(int artistId)
    {
        var albums = await _library.GetAlbumsFeaturingArtist(artistId);
        return albums.Select(_mapper.MapAlbum).ToList();
    }

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        return await _library.GetAlbumTrackSourceIds(albumId);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _library.GetTrackSourceIds(source);
    }

    public async Task<ArtistDetailsDTO> GetArtist(int id)
    {
        var artist = await _library.GetArtist(id);
        return _mapper.MapArtist(artist);
    }

    public async Task<List<AlbumDTO>> GetArtistAlbums(int artistId)
    {
        var albums = await _library.GetArtistAlbums(artistId);
        return albums.Select(_mapper.MapAlbum).ToList();
    }

    public async Task<List<ArtistDTO>> GetArtistsByGenre(string genre)
    {
        var artists = await _library.GetArtistsByGenre(genre);
        return artists.Select(_mapper.MapArtist).ToList();
    }

    public async Task<List<ArtistDTO>> GetArtistsByGrouping(int groupingId)
    {
        var artists = await _library.GetArtistsByGrouping(groupingId);
        return artists.Select(_mapper.MapArtist).ToList();
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        return await _library.GetArtistTrackSourceIds(artistId);
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

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        return await _library.GetTrackIdFromSource(trackId);
    }
}
