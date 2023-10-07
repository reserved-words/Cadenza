namespace Cadenza.Web.Database.Services;

internal class ViewModelMapper : IViewModelMapper
{

    public AlbumDetailsVM Map(AlbumDetailsDTO dto)
    {
        return new AlbumDetailsVM
        {
            ArtistId = dto.ArtistId,
            ArtistName = dto.ArtistName,
            ArtworkBase64 = dto.ArtworkBase64,
            DiscCount = dto.DiscCount,
            Id = dto.Id,
            ReleaseType = dto.ReleaseType,
            Tags = Map(dto.Tags),
            Title = dto.Title,
            TrackCounts = dto.TrackCounts,
            Year = dto.Year
        };
    }

    public AlbumTrackVM Map(AlbumTrackDTO dto)
    {
        return new AlbumTrackVM
        {
            ArtistId = dto.ArtistId,
            ArtistName = dto.ArtistName,
            Title = dto.Title,
            DiscNo = dto.DiscNo,
            DurationSeconds = dto.DurationSeconds,
            TrackId = dto.TrackId,
            TrackNo = dto.TrackNo
        };
    }

    public AlbumVM Map(AlbumDTO dto)
    {
        return new AlbumVM
        {
            Title = dto.Title,
            ArtistId = dto.ArtistId,
            ArtistName = dto.ArtistName,
            ArtworkBase64 = dto.ArtworkBase64,
            Id = dto.Id,
            ReleaseType = dto.ReleaseType,
            Year = dto.Year
        };
    }

    public ArtistVM Map(ArtistDTO dto)
    {
        return new ArtistVM
        {
            Id = dto.Id,
            Genre = dto.Genre,
            Grouping = Map(dto.Grouping),
            Name = dto.Name
        };
    }

    public ArtistDetailsVM Map(ArtistDetailsDTO dto)
    {
        return new ArtistDetailsVM
        {
            Name = dto.Name,
            Grouping = Map(dto.Grouping),
            Genre = dto.Genre,
            Id = dto.Id,
            City = dto.City,
            Country = dto.Country,
            ImageBase64 = dto.ImageBase64,
            State = dto.State,
            Tags = Map(dto.Tags)
        };
    }

    public TrackVM Map(TrackDTO dto)
    {
        return new TrackVM
        {
            AlbumId = dto.AlbumId,
            ArtistId = dto.ArtistId,
            ArtistName = dto.ArtistName,
            DurationSeconds = dto.DurationSeconds,
            Id = dto.Id,
            IdFromSource = dto.IdFromSource,
            Source = dto.Source,
            Title = dto.Title
        };
    }

    public PlayerItemVM Map(PlayerItemDTO dto)
    {
        return new PlayerItemVM
        {
            Id = dto.Id,
            Album = dto.Album,
            AlbumDisplay = dto.AlbumDisplay,
            Artist = dto.Artist,
            Name = dto.Name,
            Type = dto.Type
        };
    }

    public TrackFullVM Map(TrackFullDTO dto)
    {
        return new TrackFullVM
        {
            Artist = Map(dto.Artist),
            Album = Map(dto.Album),
            AlbumArtist = Map(dto.AlbumArtist),
            AlbumTrack = Map(dto.AlbumTrack),
            Track = Map(dto.Track)
        };
    }

    public TrackDetailsVM Map(TrackDetailsDTO dto)
    {
        return new TrackDetailsVM
        {
            AlbumId = dto.AlbumId,
            ArtistId = dto.ArtistId,
            ArtistName = dto.ArtistName,
            DurationSeconds = dto.DurationSeconds,
            Id = dto.Id,
            IdFromSource = dto.IdFromSource,
            Source = dto.Source,
            Title = dto.Title,
            Lyrics = dto.Lyrics,
            Tags = Map(dto.Tags),
            Year = dto.Year
        };
    }

    private AlbumTrackLinkVM Map(AlbumTrackLinkDTO dto)
    {
        return new AlbumTrackLinkVM
        {
            AlbumId = dto.AlbumId,
            DiscNo = dto.DiscNo,
            TrackId = dto.TrackId,
            TrackNo = dto.TrackNo
        };
    }

    public GroupingVM Map(GroupingDTO dto)
    {
        return new GroupingVM(dto.Id, dto.Name);
    }

    private TagListVM Map(TagListDTO dto)
    {
        return new TagListVM(dto.ToString());
    }

    public RecentAlbumVM Map(RecentAlbumDTO dto)
    {
        return new RecentAlbumVM
        {
            ArtistName = dto.ArtistName,
            Id = dto.Id,
            Title = dto.Title
        };
    }
}
