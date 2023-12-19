using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class LibraryMapper : ILibraryMapper
{
    private readonly IGenreMapper _genreMapper;

    public LibraryMapper(IGenreMapper genreMapper)
    {
        _genreMapper = genreMapper;
    }

    public AlbumDTO MapAlbum(GetAlbumResult album)
    {
        return new AlbumDTO
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = (ReleaseType)album.ReleaseTypeId,
            Year = album.Year
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

    public ArtistDTO MapArtist(GetArtistResult artist)
    {
        return new ArtistDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping,
            Genre = _genreMapper.MapGenreId(artist.Grouping, artist.Genre)
        };
    }

    public ArtistFullDTO MapArtist(GetFullArtistResult artist)
    {
        var dto = new ArtistDetailsDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping,
            Genre = _genreMapper.MapGenreId(artist.Grouping, artist.Genre),
            City = artist.City,
            State = artist.State,
            Country = artist.Country,
            Tags = new TagsDTO(artist.TagList)
        };

        return new ArtistFullDTO { Artist = dto };
    }

    public ArtistDTO MapArtist(GetArtistsByGroupingResult artist)
    {
        return new ArtistDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping,
            Genre = _genreMapper.MapGenreId(artist.Grouping, artist.Genre)
        };
    }

    public ArtistDTO MapArtist(GetArtistsByGenreResult artist)
    {
        return new ArtistDTO
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping,
            Genre = _genreMapper.MapGenreId(artist.Grouping, artist.Genre),
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

    public TrackFullDTO MapTrack(GetFullTrackResult track)
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
                Grouping = track.ArtistGrouping,
                Genre = _genreMapper.MapGenreId(track.ArtistGrouping, track.ArtistGenre),
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
                Grouping = track.AlbumArtistGrouping,
                Genre = _genreMapper.MapGenreId(track.AlbumArtistGrouping, track.AlbumArtistGenre),
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

    public AlbumFullDTO MapAlbum(GetFullAlbumResult album)
    {
        return new AlbumFullDTO
        {
            Album = new AlbumDetailsDTO
            {
                Id = album.Id,
                ArtistId = album.ArtistId,
                ArtistName = album.ArtistName,
                Title = album.Title,
                ReleaseType = (ReleaseType)album.ReleaseTypeId,
                DiscCount = album.DiscCount,
                Year = album.Year,
                Tags = new TagsDTO(album.TagList)
            },
            Artist = new ArtistDTO
            {
                Id = album.ArtistId,
                Name = album.ArtistName,
                Grouping = album.ArtistGrouping,
                Genre = _genreMapper.MapGenreId(album.ArtistGrouping, album.ArtistGenre)
            }
        };
    }

    public TrackDetailsDTO MapTrack(GetTrackResult track)
    {
        return new TrackDetailsDTO
        {
            Source = (LibrarySource)track.SourceId,
            Id = track.Id,
            IdFromSource = track.IdFromSource,
            IsLoved = track.IsLoved,
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

    public GenreDTO MapGenre(string grouping, string genre, List<GetArtistsByGenreResult> artists)
    {
        return new GenreDTO
        {
            Genre = _genreMapper.MapGenreId(grouping, genre),
            Grouping = grouping,
            Artists = artists.Select(MapArtist).ToList()
        };
    }
}
