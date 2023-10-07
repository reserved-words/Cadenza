namespace Cadenza.Web.Database.Services;

internal class DataTransferObjectMapper : IDataTransferObjectMapper
{
    public UpdateAlbumDTO Map(EditableAlbum vm)
    {
        return new UpdateAlbumDTO
        {
            Id = vm.Id,
            Title = vm.Title,
            ReleaseType = vm.ReleaseType,
            Year = vm.Year,
            ArtworkBase64 = vm.ArtworkBase64,
            Tags = vm.Tags
        };
    }

    public UpdateArtistDTO Map(EditableArtist vm)
    {
        return new UpdateArtistDTO
        {
            Id = vm.Id,
            GroupingId = vm.Grouping.Id,
            Genre = vm.Genre,
            ImageBase64 = vm.ImageBase64,
            Country = vm.Country,
            State = vm.State,
            City = vm.City,
            Tags = vm.Tags
        };
    }

    public UpdateTrackDTO Map(EditableTrack vm)
    {
        return new UpdateTrackDTO
        {
            Id = vm.Id,
            Title = vm.Title,
            Year = vm.Year,
            Lyrics = vm.Lyrics,
            Tags = vm.Tags
        };
    }
}
