namespace Cadenza.Database.SqlLibrary.Repositories;

internal class MusicRepository : IMusicRepository
{
    private readonly IAdmin _admin;
    private readonly IImageConverter _imageConverter;
    private readonly IDataMapper _mapper;
    private readonly ILibrary _library;
    private readonly IUpdate _update;

    public MusicRepository(IDataMapper mapper, ILibrary library, IImageConverter imageConverter, IAdmin admin, IUpdate update)
    {
        _mapper = mapper;
        _library = library;
        _imageConverter = imageConverter;
        _admin = admin;
        _update = update;
    }

    public async Task AddTrack(LibrarySource source, SyncTrackDTO track)
    {
        var trackArtistData = _mapper.MapTrackArtist(track);
        var trackArtistId = await _update.AddArtist(trackArtistData);

        var albumArtistId = trackArtistId;

        if (track.Artist.Name != track.Album.ArtistName)
        {
            var albumArtistData = _mapper.MapAlbumArtist(track);
            albumArtistId = await _update.AddArtist(albumArtistData);
        }

        var albumData = _mapper.MapAlbum(track, source, albumArtistId);
        var albumId = await _update.AddAlbum(albumData);

        var discData = _mapper.MapDisc(track, albumId);
        var discId = await _update.AddDisc(discData);

        var trackData = _mapper.MapTrack(track, trackArtistId, discId);
        await _update.AddTrack(trackData);
    }

    public async Task<FullLibraryDTO> Get()
    {
        var artistsData = await _library.GetArtists();
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

    public async Task<List<string>> GetAlbumTrackSourceIds(int albumId)
    {
        return await _library.GetAlbumTrackSourceIds(albumId);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        return await _library.GetTrackSourceIds(source);
    }

    public async Task<List<string>> GetArtistTrackSourceIds(int artistId)
    {
        return await _library.GetArtistTrackSourceIds(artistId);
    }

    public async Task<string> GetTrackIdFromSource(int trackId)
    {
        return await _library.GetTrackIdFromSource(trackId);
    }

    public async Task RemoveTrack(int id)
    {
        await _update.DeleteTrack(id);
        await _update.DeleteEmptyDiscs();
        await _update.DeleteEmptyAlbums();
        await _update.DeleteEmptyArtists();
    }

    public async Task RemoveTracks(List<string> idsFromSource)
    {
        foreach (var idFromSource in idsFromSource)
        {
            await _update.DeleteTrack(idFromSource);
        }

        await _update.DeleteEmptyDiscs();
        await _update.DeleteEmptyAlbums();
        await _update.DeleteEmptyArtists();
    }

    public async Task UpdateAlbum(ItemUpdateRequestDTO request)
    {
        var album = await _library.GetAlbum(request.Id);

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.AlbumTags:
                    album.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.AlbumTitle:
                    album.Title = update.UpdatedValue;
                    break;
                case ItemProperty.AlbumArtwork:
                    var image = _imageConverter.GetImageFromBase64Url(update.UpdatedValue);
                    album.ArtworkMimeType = image.MimeType;
                    album.ArtworkContent = image.Bytes;
                    break;
                case ItemProperty.AlbumReleaseType:
                    album.ReleaseTypeId = (int)Enum.Parse<ReleaseType>(update.UpdatedValue);
                    break;
                case ItemProperty.AlbumReleaseYear:
                    album.Year = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _update.UpdateAlbum(album);
    }

    public async Task UpdateArtist(ItemUpdateRequestDTO request)
    {
        var artist = await _library.GetArtist(request.Id);

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.ArtistImage:
                    var image = _imageConverter.GetImageFromBase64Url(update.UpdatedValue);
                    artist.ImageMimeType = image.MimeType;
                    artist.ImageContent = image.Bytes;
                    break;
                case ItemProperty.ArtistTags:
                    artist.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistCity:
                    artist.City = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistCountry:
                    artist.Country = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistGenre:
                    artist.Genre = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistGrouping:
                    var groupings = await _admin.GetGroupings();
                    var grouping = groupings.Single(g => g.Name == update.UpdatedValue);
                    artist.GroupingId = grouping.Id;
                    break;
                case ItemProperty.ArtistState:
                    artist.State = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _update.UpdateArtist(artist);
    }

    public async Task UpdateTrack(ItemUpdateRequestDTO request)
    {
        var track = await _library.GetTrack(request.Id);

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.TrackDiscNo:
                    track.DiscIndex = int.Parse(update.UpdatedValue);
                    break;
                case ItemProperty.TrackLyrics:
                    track.Lyrics = update.UpdatedValue;
                    break;
                case ItemProperty.TrackNo:
                    track.TrackNo = int.Parse(update.UpdatedValue);
                    break;
                case ItemProperty.TrackTags:
                    track.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.TrackTitle:
                    track.Title = update.UpdatedValue;
                    break;
                case ItemProperty.TrackYear:
                    track.Year = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _update.UpdateTrack(track);
    }

    private async Task AddSource(FullLibraryDTO library, LibrarySource source)
    {
        var albumsData = await _library.GetAlbums(source);
        var discsData = await _library.GetDiscs(source);
        var tracksData = await _library.GetTracks(source);

        var albums = albumsData.Select(a => _mapper.MapAlbum(a, discsData.Where(d => d.AlbumId == a.Id).ToList())).ToList();
        var albumTracks = tracksData.Select(t => _mapper.MapAlbumTrack(t)).ToList();
        var tracks = tracksData.Select(t => _mapper.MapTrack(t)).ToList();

        library.Albums.AddRange(albums);
        library.Tracks.AddRange(tracks);
        library.AlbumTracks.AddRange(albumTracks);
    }
}
