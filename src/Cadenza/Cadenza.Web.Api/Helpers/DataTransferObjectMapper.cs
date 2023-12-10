namespace Cadenza.Web.Api.Helpers;

internal class DataTransferObjectMapper : IDataTransferObjectMapper
{
    public UpdatedAlbumPropertiesDTO MapUpdate(AlbumDetailsVM vm)
    {
        return new UpdatedAlbumPropertiesDTO
        {
            AlbumId = vm.Id,
            DiscCount = vm.DiscCount,
            ArtworkBase64 = vm.ArtworkBase64,
            ReleaseType = vm.ReleaseType,
            Tags = Map(vm.Tags),
            Title = vm.Title,
            Year = vm.Year
        };
    }

    public UpdatedArtistPropertiesDTO MapUpdate(ArtistDetailsVM vm)
    {
        return new UpdatedArtistPropertiesDTO
        {
            ArtistId = vm.Id,
            GroupingName = vm.Grouping.Name,
            Genre = vm.Genre,
            City = vm.City,
            Country = vm.Country,
            State = vm.State,
            Tags = Map(vm.Tags),
            ImageBase64 = vm.ImageBase64
        };
    }

    public UpdatedTrackPropertiesDTO MapUpdate(TrackDetailsVM vm)
    {
        return new UpdatedTrackPropertiesDTO
        {
            TrackId = vm.Id,
            Title = vm.Title,
            Lyrics = vm.Lyrics,
            Year = vm.Year,
            Tags = Map(vm.Tags)
        };
    }

    public List<UpdatedAlbumTrackPropertiesDTO> MapAlbumTracks(IReadOnlyCollection<AlbumDiscVM> discs)
    {
        var tracks = new List<UpdatedAlbumTrackPropertiesDTO>();

        foreach (var disc in discs)
        {
            foreach (var track in disc.Tracks)
            {
                tracks.Add(MapUpdateProperties(disc, track));
            }
        }

        return tracks;
    }

    private static TagsDTO Map(IReadOnlyCollection<string> vm)
    {
        return new TagsDTO { Tags = vm.ToList() };
    }

    private static UpdatedAlbumTrackPropertiesDTO MapUpdateProperties(AlbumDiscVM disc, AlbumTrackVM track)
    {
        return new UpdatedAlbumTrackPropertiesDTO
        {
            TrackId = track.TrackId,
            Title = track.Title,
            TrackNo = track.TrackNo,
            DiscNo = disc.DiscNo,
            DiscTrackCount = disc.TrackCount
        };
    }
}
