using Cadenza.Database.SqlLibrary.Model.Update;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class UpdateRepository : IUpdateRepository
{
    private readonly IAdmin _admin;
    private readonly IUpdateMapper _mapper;
    private readonly IImageConverter _imageConverter;
    private readonly ILibrary _library;
    private readonly IUpdate _update;

    public UpdateRepository(ILibrary library, IImageConverter imageConverter, IAdmin admin, IUpdate update, IUpdateMapper mapper)
    {
        _library = library;
        _imageConverter = imageConverter;
        _admin = admin;
        _update = update;
        _mapper = mapper;
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

        var updatedAlbum = new UpdateAlbumParameter
        {
            Id = album.Id,
            ArtworkMimeType = album.ArtworkMimeType,
            ArtworkContent = album.ArtworkContent,
            SourceId = album.SourceId,
            ArtistId = album.ArtistId,
            Title = album.Title,
            ReleaseTypeId = album.ReleaseTypeId,
            Year = album.Year,
            DiscCount = album.DiscCount,
            TagList = album.TagList
        };

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.AlbumTags:
                    updatedAlbum.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.AlbumTitle:
                    updatedAlbum.Title = update.UpdatedValue;
                    break;
                case ItemProperty.AlbumArtwork:
                    var image = _imageConverter.GetImageFromBase64Url(update.UpdatedValue);
                    updatedAlbum.ArtworkMimeType = image.MimeType;
                    updatedAlbum.ArtworkContent = image.Bytes;
                    break;
                case ItemProperty.AlbumReleaseType:
                    updatedAlbum.ReleaseTypeId = (int)Enum.Parse<ReleaseType>(update.UpdatedValue);
                    break;
                case ItemProperty.AlbumReleaseYear:
                    updatedAlbum.Year = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _update.UpdateAlbum(updatedAlbum);
    }

    public async Task UpdateArtist(ItemUpdateRequestDTO request)
    {
        var artist = await _library.GetArtist(request.Id);

        var updatedArtist = new UpdateArtistParameter
        {
            Id = artist.Id,
            ImageMimeType = artist.ImageMimeType,
            ImageContent = artist.ImageContent,
            Name = artist.Name,
            GroupingId = artist.GroupingId,
            Genre = artist.Genre,
            City = artist.City,
            State = artist.State,
            Country = artist.Country,
            TagList = artist.TagList
        };

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.ArtistImage:
                    var image = _imageConverter.GetImageFromBase64Url(update.UpdatedValue);
                    updatedArtist.ImageMimeType = image.MimeType;
                    updatedArtist.ImageContent = image.Bytes;
                    break;
                case ItemProperty.ArtistTags:
                    updatedArtist.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistCity:
                    updatedArtist.City = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistCountry:
                    updatedArtist.Country = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistGenre:
                    updatedArtist.Genre = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistGrouping:
                    var groupings = await _admin.GetGroupings();
                    var grouping = groupings.Single(g => g.Name == update.UpdatedValue);
                    updatedArtist.GroupingId = grouping.Id;
                    break;
                case ItemProperty.ArtistState:
                    updatedArtist.State = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _update.UpdateArtist(updatedArtist);
    }

    public async Task UpdateTrack(ItemUpdateRequestDTO request)
    {
        var track = await _library.GetTrack(request.Id);

        var updatedTrack = new UpdateTrackParameter
        {
            Id = track.Id,
            DiscIndex = track.DiscIndex,
            IdFromSource = track.IdFromSource,
            ArtistId = track.ArtistId,
            DiscId = track.DiscId,
            TrackNo = track.TrackNo,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = track.Year,  
            Lyrics = track.Lyrics,
            TagList = track.TagList
        };

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.TrackDiscNo:
                    updatedTrack.DiscIndex = int.Parse(update.UpdatedValue);
                    break;
                case ItemProperty.TrackLyrics:
                    updatedTrack.Lyrics = update.UpdatedValue;
                    break;
                case ItemProperty.TrackNo:
                    updatedTrack.TrackNo = int.Parse(update.UpdatedValue);
                    break;
                case ItemProperty.TrackTags:
                    updatedTrack.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.TrackTitle:
                    updatedTrack.Title = update.UpdatedValue;
                    break;
                case ItemProperty.TrackYear:
                    updatedTrack.Year = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _update.UpdateTrack(updatedTrack);
    }
}
