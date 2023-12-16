using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class LibraryMapper : ILibraryMapper
{
    public AlbumDetailsDTO MapAlbum(GetAlbumResult album, List<GetAlbumDiscsResult> discs)
    {
        return new AlbumDetailsDTO
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = (ReleaseType)album.ReleaseTypeId,
            DiscCount = album.DiscCount,
            Year = album.Year,
            Tags = new TagsDTO(album.TagList)
        };
    }

    public AlbumDTO MapAlbum(GetArtistAlbumsResult album)
    {
        return new AlbumDTO
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = (ReleaseType)album.ReleaseTypeId,
            Year = album.Year,
        };
    }

    public AlbumDTO MapAlbum(GetAlbumsFeaturingArtistResult album)
    {
        return new AlbumDTO
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = (ReleaseType)album.ReleaseTypeId,
            Year = album.Year,
        };
    }

    public List<AlbumDiscDTO> MapAlbumTracks(int id, List<GetAlbumDiscsResult> discs, List<GetAlbumTracksResult> tracks)
    {
        return discs.Select(d => new AlbumDiscDTO
        {
            DiscNo = d.DiscNo,
            TrackCount = d.TrackCount,
            Tracks = tracks
                    .Where(t => t.DiscNo == d.DiscNo)
                    .Select(t => new AlbumTrackDTO
                    {
                        TrackId = t.TrackId,
                        IdFromSource = t.IdFromSource,
                        Title = t.Title,
                        ArtistId = t.ArtistId,
                        ArtistName = t.ArtistName,
                        DurationSeconds = t.DurationSeconds,
                        DiscNo = t.DiscNo,
                        TrackNo = t.TrackNo
                    })
                    .OrderBy(t => t.TrackNo)
                    .ToList()
            })
            .OrderBy(d => d.DiscNo)
            .ToList();
    }

    public ArtistDetailsDTO MapArtist(GetArtistResult artist)
    {
        return new ArtistDetailsDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = MapGrouping(artist),
            Genre = artist.Genre,
            City = artist.City,
            State = artist.State,
            Country = artist.Country,
            Tags = new TagsDTO(artist.TagList)
        };
    }

    public ArtistDTO MapArtist(GetArtistsByGroupingResult artist)
    {
        return new ArtistDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = MapGrouping(artist),
            Genre = artist.Genre
        };
    }

    public ArtistDTO MapArtist(GetArtistsByGenreResult artist)
    {
        return new ArtistDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = MapGrouping(artist),
            Genre = artist.Genre
        };
    }

    public TaggedItemDTO MapTaggedItem(GetTaggedItemsResult result)
    {
        var type = Enum.Parse<PlayerItemType>(result.Type);

        var albumDisplay = type == PlayerItemType.Track
            ? result.Album + (result.AlbumArtist == result.Artist ? "" : $" ({result.AlbumArtist})")
            : null;

        return new TaggedItemDTO
        {
            Type = type,
            Id = result.Id,
            Name = result.Name,
            Artist = result.Artist,
            Album = result.Album,
            AlbumDisplay = albumDisplay
        };
    }

    public TrackFullDTO MapTrack(GetTrackResult track)
    {
        return new TrackFullDTO
        {
            Track = new TrackDetailsDTO
            {
                Source = (LibrarySource)track.SourceId,
                Id = track.Id,
                IdFromSource = track.IdFromSource,
                IsLoved = track.IsLoved,
                ArtistId = track.ArtistId,
                ArtistName = track.ArtistName,
                AlbumId = track.AlbumId,
                Title = track.TrackTitle,
                DurationSeconds = track.DurationSeconds,
                Year = track.TrackYear,
                Lyrics = track.Lyrics,
                Tags = new TagsDTO(track.TrackTagList)
            },
            Artist = new ArtistDetailsDTO
            {
                Id = track.ArtistId,
                Name = track.ArtistName,
                Grouping = MapArtistGrouping(track),
                Genre = track.ArtistGenre,
                City = track.ArtistCity,
                State = track.ArtistState,
                Country = track.ArtistCountry,
                Tags = new TagsDTO(track.ArtistTagList)
            },
            Album = new AlbumDetailsDTO
            {
                Id = track.AlbumId,
                ArtistId = track.AlbumArtistId,
                ArtistName = track.AlbumArtistName,
                Title = track.AlbumTitle,
                ReleaseType = (ReleaseType)track.ReleaseTypeId,
                DiscCount = track.DiscCount,
                Year = track.AlbumYear,
                Tags = new TagsDTO(track.AlbumTagList)
            },
            AlbumArtist = new ArtistDetailsDTO
            {
                Id = track.AlbumArtistId,
                Name = track.AlbumArtistName,
                Grouping = MapAlbumArtistGrouping(track),
                Genre = track.AlbumArtistGenre,
                City = track.AlbumArtistCity,
                State = track.AlbumArtistState,
                Country = track.AlbumArtistCountry,
                Tags = new TagsDTO(track.AlbumArtistTagList)
            },
            AlbumTrack = new AlbumTrackLinkDTO
            {
                TrackId = track.Id,
                AlbumId = track.AlbumId,
                DiscNo = track.DiscNo,
                TrackNo = track.TrackNo,
                DiscCount = track.DiscCount,
                TrackCount = track.TrackCount
            }
        };
    }

    private GroupingDTO MapGrouping(GetArtistsByGenreResult artist)
    {
        return new GroupingDTO
        {
            Id = artist.GroupingId,
            Name = artist.GroupingName,
            IsUsed = true
        };
    }

    private GroupingDTO MapGrouping(GetArtistResult artist)
    {
        return new GroupingDTO
        {
            Id = artist.GroupingId,
            Name = artist.GroupingName,
            IsUsed = true
        };
    }

    private GroupingDTO MapGrouping(GetArtistsByGroupingResult artist)
    {
        return new GroupingDTO
        {
            Id = artist.GroupingId,
            Name = artist.GroupingName,
            IsUsed = true
        };
    }

    private GroupingDTO MapArtistGrouping(GetTrackResult track)
    {
        return new GroupingDTO
        {
            Id = track.ArtistGroupingId,
            Name = track.ArtistGroupingName,
            IsUsed = true
        };
    }

    private GroupingDTO MapAlbumArtistGrouping(GetTrackResult track)
    {
        return new GroupingDTO
        {
            Id = track.AlbumArtistGroupingId,
            Name = track.AlbumArtistGroupingName,
            IsUsed = true
        };
    }
}
