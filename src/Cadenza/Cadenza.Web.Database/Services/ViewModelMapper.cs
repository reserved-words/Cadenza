using System.Collections.ObjectModel;

namespace Cadenza.Web.Database.Services;

internal class ViewModelMapper : IViewModelMapper
{

    public AlbumDetailsVM Map(AlbumDetailsDTO dto)
    {
        return new AlbumDetailsVM
        {
            ArtistId = dto.ArtistId,
            ArtistName = dto.ArtistName,
            Id = dto.Id,
            ReleaseType = dto.ReleaseType,
            Tags = Map(dto.Tags),
            Title = dto.Title,
            Year = dto.Year
        };
    }

    public AlbumTracksVM Map(AlbumTracksDTO dto)
    {
        return new AlbumTracksVM
        {
            AlbumId = dto.AlbumId,
            Discs = dto.Discs.Select(d => Map(d)).ToList()
        };
    }

    public AlbumVM Map(AlbumDTO dto)
    {
        return new AlbumVM
        {
            Title = dto.Title,
            ArtistId = dto.ArtistId,
            ArtistName = dto.ArtistName,
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

    public SearchItemVM Map(SearchItemDTO dto)
    {
        return new SearchItemVM(dto.Type, dto.Id, dto.Name, dto.Artist, dto.Album, dto.AlbumDisplay);
    }

    public TaggedItemVM Map(TaggedItemDTO dto)
    {
        return new TaggedItemVM(dto.Type, dto.Id, dto.Name, dto.Artist, dto.Album, dto.AlbumDisplay);
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
        return new AlbumTrackLinkVM(dto.AlbumId, dto.TrackId, dto.DiscNo, dto.TrackNo, dto.DiscCount, dto.TrackCount);
    }

    public GroupingVM Map(GroupingDTO dto)
    {
        return new GroupingVM(dto.Id, dto.Name);
    }

    private IReadOnlyCollection<string> Map(TagsDTO dto)
    {
        return new ReadOnlyCollection<string>(dto.Tags);
    }

    public RecentAlbumVM Map(RecentAlbumDTO dto)
    {
        return new RecentAlbumVM(dto.Id, dto.Title, dto.ArtistName);
    }

    private AlbumDiscVM Map(AlbumDiscDTO dto)
    {
        return new AlbumDiscVM
        {
            DiscNo = dto.DiscNo,
            TrackCount = dto.TrackCount,
            Tracks = dto.Tracks.Select(t => Map(t)).ToList()
        };
    }

    private AlbumTrackVM Map(AlbumTrackDTO dto)
    {
        return new AlbumTrackVM
        {
            ArtistId = dto.ArtistId,
            ArtistName = dto.ArtistName,
            Title = dto.Title,
            DurationSeconds = dto.DurationSeconds,
            TrackId = dto.TrackId,
            TrackNo = dto.TrackNo,
            IdFromSource = dto.IdFromSource
        };
    }
}
