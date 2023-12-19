using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Update;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class UpdateMapper : IUpdateMapper
{
    private const string DefaultGenre = "None";
    private const string DefaultGrouping = "None";

    private readonly INameComparer _nameComparer;

    public UpdateMapper(INameComparer nameComparer)
    {
        _nameComparer = nameComparer;
    }

    public AddAlbumParameter MapAlbum(SyncTrackDTO track, LibrarySource source, int artistId)
    {
        return new AddAlbumParameter
        {
            SourceId = (int)source,
            ArtistId = artistId,
            Title = track.Album.Title,
            ReleaseTypeId = (int)track.Album.ReleaseType,
            Year = track.Album.Year,
            DiscCount = track.Album.DiscCount,
            TagList = track.Album.TagList,
            ArtworkMimeType = track.Album.ArtworkMimeType,
            ArtworkContent = track.Album.ArtworkContent
        };
    }

    public AddArtistParameter MapAlbumArtist(SyncTrackDTO track)
    {
        return new AddArtistParameter
        {
            Name = track.Album.ArtistName,
            CompareName = _nameComparer.GetCompareName(track.Album.ArtistName),
            Grouping = DefaultGrouping,
            Genre = DefaultGenre
        };
    }

    public AddDiscParameter MapDisc(SyncTrackDTO track, int albumId)
    {
        return new AddDiscParameter
        {
            AlbumId = albumId,
            DiscNo = track.DiscNo,
            TrackCount = track.DiscTrackCount
        };
    }

    public AddTrackParameter MapTrack(SyncTrackDTO track, int artistId, int discId)
    {
        return new AddTrackParameter
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

    public AddArtistParameter MapTrackArtist(SyncTrackDTO track)
    {
        return new AddArtistParameter
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

    public UpdateAlbumParameter MapAlbumToUpdate(int id, GetAlbumForUpdateResult album)
    {
        return new UpdateAlbumParameter
        {
            Id = id,
            ArtworkMimeType = album.ArtworkMimeType,
            ArtworkContent = album.ArtworkContent,
            ArtistId = album.ArtistId,
            Title = album.Title,
            ReleaseTypeId = album.ReleaseTypeId,
            Year = album.Year,
            TagList = album.TagList
        };
    }

    public UpdateTrackParameter MapTrackToUpdate(int id, GetTrackForUpdateResult track)
    {
        return new UpdateTrackParameter
        {
            Id = id,
            DiscNo = track.DiscNo,
            TrackNo = track.TrackNo,
            Title = track.Title,
            Year = track.Year,
            Lyrics = track.Lyrics,
            TagList = track.TagList,
            DiscTrackCount = track.DiscTrackCount
        };
    }

    private static string ValueOrDefault(string value, string defaultValue)
    {
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }
}
