﻿namespace Cadenza.Database.SqlLibrary.Services;

internal class LibraryReader : ILibraryReader
{
    private readonly IDataMapper _mapper;
    private readonly IDataReadService _readService;

    public LibraryReader(IDataMapper mapper, IDataReadService readService)
    {
        _mapper = mapper;
        _readService = readService;
    }

    public async Task<FullLibraryDTO> Get()
    {
        var artistsData = await _readService.GetArtists();
        var artists = artistsData.Select(a => _mapper.MapArtist(a)).ToList();

        var library = new FullLibraryDTO
        {
            Artists = artists,
            Albums = new List<AlbumDetailsDTO>(),
            Tracks = new List<TrackDetailsDTO>(),
            AlbumTracks = new List<AlbumTrackLinkDTO>()
        };

        foreach (var src in Enum.GetValues<LibrarySource>())
        {
            await AddSource(library, src);
        }

        return library;
    }

    public async Task<List<int>> GetAbumTrackIds(int albumId)
    {
        return await _readService.GetAbumTrackIds(albumId);
    }

    public async Task<ArtworkImage> GetAlbumArtwork(int id)
    {
        var data = await _readService.GetAlbumArtwork(id);
        if (data?.Content == null)
            return null;

        return new ArtworkImage(data.Content, data.MimeType);
    }

    public Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        return _readService.GetAlbumTrackSourceIds(albumId);
    }

    public async Task<List<int>> GetAllTrackIds()
    {
        return await _readService.GetAllTrackIds();
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _readService.GetAllTrackSourceIds(source);
    }

    public async Task<ArtworkImage> GetArtistImage(int id)
    {
        var data = await _readService.GetArtistImage(id);
        if (data?.Content == null)
            return null;

        return new ArtworkImage(data.Content, data.MimeType);
    }

    public async Task<List<int>> GetArtistTrackIds(int artistId)
    {
        return await _readService.GetArtistTrackIds(artistId);
    }

    public Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        return _readService.GetArtistTrackSourceIds(artistId);
    }

    public async Task<List<int>> GetGenreTrackIds(string genre)
    {
        return await _readService.GetGenreTrackIds(genre);
    }

    public async Task<List<int>> GetGroupingTrackIds(int groupingId)
    {
        return await _readService.GetGroupingTrackIds(groupingId);
    }

    public async Task<List<int>> GetTagTrackIds(string tag)
    {
        return await _readService.GetTagTrackIds(tag);
    }

    public Task<string> GetTrackIdFromSource(int trackId)
    {
        return _readService.GetTrackIdFromSource(trackId);
    }

    private async Task AddSource(FullLibraryDTO library, LibrarySource source)
    {
        var albumsData = await _readService.GetAlbums(source);
        var discsData = await _readService.GetDiscs(source);
        var tracksData = await _readService.GetTracks(source);

        var albums = albumsData.Select(a => _mapper.MapAlbum(a, discsData.Where(d => d.AlbumId == a.Id).ToList())).ToList();
        var albumTracks = tracksData.Select(t => _mapper.MapAlbumTrack(t)).ToList();
        var tracks = tracksData.Select(t => _mapper.MapTrack(t)).ToList();

        library.Albums.AddRange(albums);
        library.Tracks.AddRange(tracks);
        library.AlbumTracks.AddRange(albumTracks);
    }
}