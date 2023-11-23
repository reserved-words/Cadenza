namespace Cadenza.Database.SqlLibrary.Services;
internal class Mapper : IMapper
{
    private const string DefaultGenre = "None";

    private readonly INameComparer _nameComparer;

    public Mapper(INameComparer nameComparer)
    {
        _nameComparer = nameComparer;
    }

    public NewAlbumData MapAlbum(SyncTrackDTO track, LibrarySource source, int artistId)
    {
        return new NewAlbumData
        {
            SourceId = (int)source,
            ArtistId = artistId,
            Title = track.Album.Title,
            ReleaseTypeId = (int)track.Album.ReleaseType,
            Year = track.Album.Year,
            DiscCount = track.Album.TrackCounts.Count,
            TagList = track.Album.TagList,
            ArtworkMimeType = track.Album.ArtworkMimeType,
            ArtworkContent = track.Album.ArtworkContent
        };
    }

    public NewArtistData MapAlbumArtist(SyncTrackDTO track)
    {
        return new NewArtistData
        {
            Name = track.Album.ArtistName,
            CompareName = _nameComparer.GetCompareName(track.Album.ArtistName),
            GroupingId = 0,
            Genre = DefaultGenre
        };
    }

    public NewDiscData MapDisc(SyncTrackDTO track, int albumId)
    {
        var index = track.DiscNo;
        if (index <= 0)
            index = 1;

        var trackCount = track.Album.TrackCounts.Count >= index
            ? track.Album.TrackCounts[index - 1]
            : 0;

        return new NewDiscData
        {
            AlbumId = albumId,
            Index = index,
            TrackCount = trackCount
        };
    }

    public NewTrackData MapTrack(SyncTrackDTO track, int artistId, int discId)
    {
        return new NewTrackData
        {
            IdFromSource = track.IdFromSource,
            ArtistId = artistId,
            DiscId = discId,
            TrackNo = track.TrackNo,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = track.Year,
            Lyrics = track.Lyrics,
            TagList = track.TagList
        };
    }

    public NewArtistData MapTrackArtist(SyncTrackDTO track)
    {
        return new NewArtistData
        {
            Name = track.Artist.Name,
            CompareName = _nameComparer.GetCompareName(track.Artist.Name),
            GroupingName = track.Artist.Grouping,
            Genre = ValueOrDefault(track.Artist.Genre, DefaultGenre),
            City = track.Artist.City,
            State = track.Artist.State,
            Country = track.Artist.Country,
            TagList = track.Artist.TagList,
            ImageMimeType = track.Artist.ImageMimeType,
            ImageContent = track.Artist.ImageContent
        };
    }

    public AlbumDetailsDTO MapAlbum(GetAlbumData album, List<GetDiscData> discs)
    {
        return new AlbumDetailsDTO
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = (ReleaseType)album.ReleaseTypeId,
            Year = album.Year,
            DiscCount = album.DiscCount,
            DiscTrackCounts = discs.ToDictionary(d => d.Index, d => d.TrackCount),
            Tags = new TagsDTO(album.TagList)
        };
    }

    public AlbumTrackLinkDTO MapAlbumTrack(GetTrackData track)
    {
        return new AlbumTrackLinkDTO
        {
            TrackId = track.Id,
            AlbumId = track.AlbumId,
            DiscNo = track.DiscIndex,
            TrackNo = track.TrackNo
        };
    }

    public ArtistDetailsDTO MapArtist(GetArtistData artist)
    {
        return new ArtistDetailsDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = new GroupingDTO(artist.GroupingId, artist.GroupingName),
            Genre = artist.Genre,
            City = artist.City,
            State = artist.State,
            Country = artist.Country,
            Tags = new TagsDTO(artist.TagList)
        };
    }

    public TrackDetailsDTO MapTrack(GetTrackData track)
    {
        return new TrackDetailsDTO
        {
            Source = track.SourceId,
            Id = track.Id,
            IdFromSource = track.IdFromSource,
            ArtistId = track.ArtistId,
            ArtistName = track.ArtistName,
            AlbumId = track.AlbumId,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = track.Year,
            Lyrics = track.Lyrics,
            Tags = new TagsDTO(track.TagList)
        };
    }

    private static string ValueOrDefault(string value, string defaultValue)
    {
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }

    public RecentAlbumDTO MapRecentAlbum(RecentAlbumData data)
    {
        return new RecentAlbumDTO
        {
            Id = data.AlbumId,
            Title = data.AlbumTitle,
            ArtistName = data.ArtistName
        };
    }

    public SyncTrackRemovalRequestDTO MapSyncTrackRemovalRequest(TrackRemovalData data)
    {
        return new SyncTrackRemovalRequestDTO
        {
            RequestId = data.RequestId,
            TrackIdFromSource = data.TrackIdFromSource
        };
    }

    public NewArtistUpdateData MapArtistUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        return new NewArtistUpdateData
        {
            ArtistId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };
    }

    public NewAlbumUpdateData MapAlbumUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        return new NewAlbumUpdateData
        {
            AlbumId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };
    }

    public NewTrackUpdateData MapTrackUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        return new NewTrackUpdateData
        {
            TrackId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };
    }

    public List<ItemUpdateRequestDTO> MapAlbumUpdateRequests(List<AlbumUpdateData> data)
    {
        return data.GroupBy(d => d.AlbumId)
            .Select(a => new ItemUpdateRequestDTO
            {
                Type = LibraryItemType.Album,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdateDTO
                {
                    Id = u.Id,
                    Property = Enum.Parse<ItemProperty>(u.PropertyName),
                    OriginalValue = u.OriginalValue,
                    UpdatedValue = u.UpdatedValue
                }).ToList()
            })
            .ToList();
    }

    public List<ItemUpdateRequestDTO> MapArtistUpdateRequests(List<ArtistUpdateData> data)
    {
        return data.GroupBy(d => d.ArtistId)
            .Select(a => new ItemUpdateRequestDTO
            {
                Type = LibraryItemType.Artist,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdateDTO
                {
                    Id = u.Id,
                    Property = Enum.Parse<ItemProperty>(u.PropertyName),
                    OriginalValue = u.OriginalValue,
                    UpdatedValue = u.UpdatedValue
                }).ToList()
            })
            .ToList();
    }

    public List<ItemUpdateRequestDTO> MapTrackUpdateRequests(List<TrackUpdateData> data)
    {
        return data.GroupBy(d => d.TrackId)
            .Select(a => new ItemUpdateRequestDTO
            {
                Type = LibraryItemType.Track,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdateDTO
                {
                    Id = u.Id,
                    Property = Enum.Parse<ItemProperty>(u.PropertyName),
                    OriginalValue = u.OriginalValue,
                    UpdatedValue = u.UpdatedValue
                }).ToList()
            })
            .ToList();
    }
}
