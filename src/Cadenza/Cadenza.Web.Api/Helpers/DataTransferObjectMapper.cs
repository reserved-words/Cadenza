namespace Cadenza.Web.Api.Helpers;

internal class DataTransferObjectMapper : IDataTransferObjectMapper
{
    public UpdatedAlbumPropertiesDTO MapAlbum(AlbumDetailsVM album)
    {
        return new UpdatedAlbumPropertiesDTO
        {
            AlbumId = album.Id,
            DiscCount = album.DiscCount,
            ArtworkBase64 = album.ArtworkBase64,
            ReleaseType = album.ReleaseType,
            Tags = Map(album.Tags),
            Title = album.Title,
            Year = album.Year
        };
    }

    public UpdatedArtistPropertiesDTO MapArtist(ArtistDetailsVM artist)
    {
        return new UpdatedArtistPropertiesDTO
        {
            ArtistId = artist.Id,
            GroupingName = artist.Grouping.Name,
            Genre = artist.Genre,
            City = artist.City,
            Country = artist.Country,
            State = artist.State,
            Tags = Map(artist.Tags),
            ImageBase64 = artist.ImageBase64
        };
    }

    public UpdatedTrackPropertiesDTO MapTrack(TrackDetailsVM track)
    {
        return new UpdatedTrackPropertiesDTO
        {
            TrackId = track.Id,
            Title = track.Title,
            Lyrics = track.Lyrics,
            Year = track.Year,
            Tags = Map(track.Tags)
        };
    }

    public List<UpdatedAlbumTrackPropertiesDTO> MapAlbumTracks(IReadOnlyCollection<AlbumTrackVM> tracks)
    {
        return tracks.Select(r => new UpdatedAlbumTrackPropertiesDTO
        {
            TrackId = r.TrackId,
            Title = r.Title,
            TrackNo = r.TrackNo,
            DiscNo = r.DiscNo,
            DiscTrackCount = r.DiscTrackCount
        })
        .ToList();
    }

    private static TagsDTO Map(IReadOnlyCollection<string> tags)
    {
        return new TagsDTO { Tags = tags.ToList() };
    }

    public List<UpdatedArtistReleasePropertiesDTO> MapArtistReleases(IReadOnlyCollection<AlbumVM> releases)
    {
        return releases.Select(r => new UpdatedArtistReleasePropertiesDTO
        {
            AlbumId = r.Id,
            Title = r.Title,
            ReleaseType = r.ReleaseType,
            Year = r.Year
        })
        .ToList();
    }
}
