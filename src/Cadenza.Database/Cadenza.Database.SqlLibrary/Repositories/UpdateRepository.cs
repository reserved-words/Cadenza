using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Update;

namespace Cadenza.Database.SqlLibrary.Repositories;

internal class UpdateRepository : IUpdateRepository
{
    private readonly IAdmin _admin;
    private readonly IImageConverter _imageConverter;
    private readonly IUpdate _update;
    private readonly IUpdateMapper _mapper;

    public UpdateRepository(IImageConverter imageConverter, IAdmin admin, IUpdate update, IUpdateMapper mapper)
    {
        _imageConverter = imageConverter;
        _admin = admin;
        _update = update;
        _mapper = mapper;
    }

    public async Task AddTrack(LibrarySource source, SyncTrackDTO track)
    {
        var trackArtistData = _mapper.MapTrackArtist(track);
        var trackArtistId = await _update.AddArtist(trackArtistData);

        var albumArtistId = trackArtistId;

        if (track.Artist.Name != track.Album.ArtistName)
        {
            var albumArtistData = _mapper.MapAlbumArtist(track);
            albumArtistId = await _update.AddArtist(albumArtistData);
        }

        var albumData = _mapper.MapAlbum(track, source, albumArtistId);
        var albumId = await _update.AddAlbum(albumData);

        var discData = _mapper.MapDisc(track, albumId);
        var discId = await _update.AddDisc(discData);

        var trackData = _mapper.MapTrack(track, trackArtistId, discId);
        await _update.AddTrack(trackData);
    }

    public async Task LoveTrack(string username, int trackId)
    {
        await _update.UpdateTrackIsLoved(new UpdateTrackIsLovedParameter { Username = username, TrackId = trackId, IsLoved = true });
    }

    public async Task RemoveTrack(int id)
    {
        await _update.DeleteTrack(id);
        await _update.DeleteEmptyDiscs();
        await _update.DeleteEmptyAlbums();
        await _update.DeleteEmptyArtists();
    }

    public async Task RemoveTracks(List<string> idsFromSource)
    {
        foreach (var idFromSource in idsFromSource)
        {
            await _update.DeleteTrack(idFromSource);
        }

        await _update.DeleteEmptyDiscs();
        await _update.DeleteEmptyAlbums();
        await _update.DeleteEmptyArtists();
    }

    public async Task UnloveTrack(string username, int trackId)
    {
        await _update.UpdateTrackIsLoved(new UpdateTrackIsLovedParameter { Username = username, TrackId = trackId, IsLoved = false });
    }

    public async Task UpdateAlbum(UpdatedAlbumPropertiesDTO update)
    {
        var albumToUpdate = await _update.GetAlbumForUpdate(update.AlbumId);

        var albumUpdateParameter = new UpdateAlbumParameter
        {
            Id = update.AlbumId,
            ArtistId = albumToUpdate.ArtistId,
            Title = update.Title,
            ReleaseTypeId = (int)update.ReleaseType,
            Year = update.Year,
            DiscCount = update.DiscCount,
            ArtworkMimeType = albumToUpdate.ArtworkMimeType,
            ArtworkContent = albumToUpdate.ArtworkContent,
            TagList = update.Tags.ToString()
        };

        if (update.ArtworkBase64 != null)
        {
            var image = _imageConverter.GetImageFromBase64Url(update.ArtworkBase64);
            albumUpdateParameter.ArtworkMimeType = image.MimeType;
            albumUpdateParameter.ArtworkContent = image.Bytes;
        }

        await _update.UpdateAlbum(albumUpdateParameter);
    }

    public async Task UpdateArtist(UpdatedArtistPropertiesDTO update)
    {
        var artistToUpdate = await _update.GetArtistForUpdate(update.ArtistId);

        var artistUpdateParameter = new UpdateArtistParameter
        {
            Id = update.ArtistId,
            Grouping = update.Grouping,
            Genre = update.Genre,
            City = update.City,
            State = update.State,
            Country = update.Country,
            ImageMimeType = artistToUpdate.ImageMimeType,
            ImageContent = artistToUpdate.ImageContent,
            TagList = update.Tags.ToString()
        };

        if (update.ImageBase64 != null)
        {
            var image = _imageConverter.GetImageFromBase64Url(update.ImageBase64);
            artistUpdateParameter.ImageMimeType = image.MimeType;
            artistUpdateParameter.ImageContent = image.Bytes;
        }

        await _update.UpdateArtist(artistUpdateParameter);
    }

    public async Task UpdateAlbumTrack(UpdatedAlbumTrackPropertiesDTO update)
    {
        var track = await _update.GetTrackForUpdate(update.TrackId);

        var trackUpdateParameter = new UpdateTrackParameter
        {
            Id = update.TrackId,
            Title = update.Title,
            Year = track.Year,
            Lyrics = track.Lyrics,
            DiscNo = update.DiscNo,
            TrackNo = update.TrackNo,
            DiscTrackCount = update.DiscTrackCount,
            TagList = track.TagList
        };

        await _update.UpdateTrack(trackUpdateParameter);
    }

    public async Task UpdateTrack(UpdatedTrackPropertiesDTO update)
    {
        var track = await _update.GetTrackForUpdate(update.TrackId);

        var trackUpdateParameter = new UpdateTrackParameter
        {
            Id = update.TrackId,
            Title = update.Title,
            Year = update.Year,
            Lyrics = update.Lyrics,
            DiscNo = track.DiscNo,
            TrackNo = track.TrackNo,
            DiscTrackCount = track.DiscTrackCount,
            TagList = update.Tags.ToString()
        };

        await _update.UpdateTrack(trackUpdateParameter);
    }

    public async Task UpdateArtistRelease(UpdatedArtistReleasePropertiesDTO update)
    {
        var album = await _update.GetAlbumForUpdate(update.AlbumId);

        var albumUpdateParameter = new UpdateAlbumParameter
        {
            Id = update.AlbumId,
            ArtistId = album.ArtistId,
            Title = update.Title,
            ReleaseTypeId = (int)update.ReleaseType,
            Year = update.Year,
            DiscCount = album.DiscCount,
            ArtworkMimeType = album.ArtworkMimeType,
            ArtworkContent = album.ArtworkContent,
            TagList = album.TagList
        };

        await _update.UpdateAlbum(albumUpdateParameter);
    }
}
