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

    public List<AlbumTrackDTO> MapAlbumTracks(IReadOnlyCollection<AlbumDiscVM> discs)
    {
        var tracks = new List<AlbumTrackDTO>();

        foreach (var disc in discs)
        {
            foreach (var track in disc.Tracks)
            {
                tracks.Add(Map(disc, track));
            }
        }

        return tracks;
    }

    private static AlbumDetailsDTO Map(AlbumDetailsVM vm)
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
            DiscTrackCounts = vm.DiscTrackCounts.ToDictionary(d => d.Key, d => d.Value),
            Year = vm.Year
        };
    }

    private static ArtistDetailsDTO Map(ArtistDetailsVM vm)
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

    private static TrackDetailsDTO Map(TrackDetailsVM vm)
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
}
