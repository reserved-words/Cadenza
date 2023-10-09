namespace Cadenza.Web.Database.Services;

internal class DataTransferObjectMapper : IDataTransferObjectMapper
{
    public UpdateAlbumDTO MapUpdate(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum)
    {
        return new UpdateAlbumDTO
        {
            OriginalAlbum = Map(originalAlbum),
            UpdatedAlbum = Map(updatedAlbum)
        };
    }

    public UpdateArtistDTO MapUpdate(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist)
    {
        return new UpdateArtistDTO
        {
            OriginalArtist = Map(originalArtist),
            UpdatedArtist = Map(updatedArtist)
        };
    }

    public UpdateTrackDTO MapUpdate(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack)
    {
        return new UpdateTrackDTO
        {
            OriginalTrack = Map(originalTrack),
            UpdatedTrack = Map(updatedTrack)
        };
    }

    private AlbumDetailsDTO Map(AlbumDetailsVM vm)
    {
        return new AlbumDetailsDTO
        {
            ArtistId = vm.ArtistId,
            ArtistName = vm.ArtistName,
            ArtworkBase64 = vm.ArtworkBase64,
            DiscCount = vm.DiscCount,
            Id = vm.Id,
            ReleaseType = vm.ReleaseType,
            Tags = Map(vm.Tags),
            Title = vm.Title,
            TrackCounts = vm.TrackCounts,
            Year = vm.Year
        };
    }

    private ArtistDetailsDTO Map(ArtistDetailsVM vm)
    {
        return new ArtistDetailsDTO
        {
            Name = vm.Name,
            Grouping = Map(vm.Grouping),
            Genre = vm.Genre,
            Id = vm.Id,
            City = vm.City,
            Country = vm.Country,
            ImageBase64 = vm.ImageBase64,
            State = vm.State,
            Tags = Map(vm.Tags)
        };
    }

    private TrackDetailsDTO Map(TrackDetailsVM vm)
    {
        return new TrackDetailsDTO
        {
            AlbumId = vm.AlbumId,
            ArtistId = vm.ArtistId,
            ArtistName = vm.ArtistName,
            DurationSeconds = vm.DurationSeconds,
            Id = vm.Id,
            IdFromSource = vm.IdFromSource,
            Source = vm.Source,
            Title = vm.Title
        };
    }

    public GroupingDTO Map(GroupingVM dto)
    {
        return new GroupingDTO(dto.Id, dto.Name);
    }

    private TagsDTO Map(IReadOnlyCollection<string> vm)
    {
        return new TagsDTO { Tags = vm.ToList() };
    }
}
