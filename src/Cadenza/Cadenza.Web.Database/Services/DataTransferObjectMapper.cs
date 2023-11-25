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

    private static UpdatedAlbumPropertiesDTO Map(AlbumDetailsVM vm)
    {
        return new UpdatedAlbumPropertiesDTO
        {
            Id = vm.Id,
            ArtworkBase64 = vm.ArtworkBase64,
            ReleaseType = vm.ReleaseType,
            Tags = Map(vm.Tags),
            Title = vm.Title,
            Year = vm.Year
        };
    }

    private static UpdatedArtistPropertiesDTO Map(ArtistDetailsVM vm)
    {
        return new UpdatedArtistPropertiesDTO
        {
            Id = vm.Id,
            GroupingName = vm.Grouping.Name,
            Genre = vm.Genre,
            City = vm.City,
            Country = vm.Country,
            State = vm.State,
            Tags = Map(vm.Tags),
            ImageBase64 = vm.ImageBase64
        };
    }

    private static UpdatedTrackPropertiesDTO Map(TrackDetailsVM vm)
    {
        return new UpdatedTrackPropertiesDTO
        {
            Id = vm.Id,
            Title = vm.Title,
            Lyrics = vm.Lyrics,
            Year = vm.Year,
            Tags = Map(vm.Tags)
        };
    }

    public static GroupingDTO Map(GroupingVM dto)
    {
        return new GroupingDTO(dto.Id, dto.Name);
    }

    private static TagsDTO Map(IReadOnlyCollection<string> vm)
    {
        return new TagsDTO { Tags = vm.ToList() };
    }

    private static AlbumDiscDTO Map(AlbumDiscVM disc)
    {
        return new AlbumDiscDTO
        {
            DiscNo = disc.DiscNo,
            TrackCount = disc.TrackCount,
            Tracks = disc.Tracks.Select(t => Map(disc, t)).ToList()
        };
    }

    private static AlbumTrackDTO Map(AlbumDiscVM disc, AlbumTrackVM track)
    {
        return new AlbumTrackDTO
        {
            ArtistId = track.ArtistId,
            ArtistName = track.ArtistName,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            TrackId = track.TrackId,
            TrackNo = track.TrackNo,
            DiscNo = disc.DiscNo,
            IdFromSource = track.IdFromSource
        };
    }

    private static UpdatedAlbumTrackPropertiesDTO MapUpdateProperties(AlbumDiscVM disc, AlbumTrackVM track)
    {
        return new UpdatedAlbumTrackPropertiesDTO
        {
            TrackId = track.TrackId,
            Title = track.Title,
            TrackNo = track.TrackNo,
            DiscNo = disc.DiscNo
        };
    }
}
