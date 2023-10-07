using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary.Services;

internal class LibraryUpdater : ILibraryUpdater
{
    private readonly IDataReadService _readService;
    private readonly IDataUpdateService _updateService;
    private readonly IImageConverter _imageConverter;

    public LibraryUpdater(IDataUpdateService updateService, IDataReadService readService, IImageConverter imageConverter)
    {
        _updateService = updateService;
        _readService = readService;
        _imageConverter = imageConverter;
    }

    public async Task UpdateAlbum(ItemUpdateRequestDTO request)
    {
        var album = await _readService.GetAlbum(request.Id);

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.AlbumTags:
                    album.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.AlbumTitle:
                    album.Title = update.UpdatedValue;
                    break;
                case ItemProperty.Artwork:
                    var image = _imageConverter.GetImageFromBase64Url(update.UpdatedValue);
                    album.ArtworkMimeType = image.MimeType;
                    album.ArtworkContent = image.Bytes;
                    break;
                case ItemProperty.ReleaseType:
                    album.ReleaseTypeId = (int)Enum.Parse<ReleaseType>(update.UpdatedValue);
                    break;
                case ItemProperty.ReleaseYear:
                    album.Year = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _updateService.UpdateAlbum(album);
    }

    public async Task UpdateArtist(ItemUpdateRequestDTO request)
    {
        var artist = await _readService.GetArtist(request.Id);

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.ArtistImage:
                    var image = _imageConverter.GetImageFromBase64Url(update.UpdatedValue);
                    artist.ImageMimeType = image.MimeType;
                    artist.ImageContent = image.Bytes;
                    break;
                case ItemProperty.ArtistTags:
                    artist.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.City:
                    artist.City = update.UpdatedValue;
                    break;
                case ItemProperty.Country:
                    artist.Country = update.UpdatedValue;
                    break;
                case ItemProperty.Genre:
                    artist.Genre = update.UpdatedValue;
                    break;
                case ItemProperty.Grouping:
                    var groupings = await _readService.GetGroupings();
                    var grouping = groupings.Single(g => g.Name == update.UpdatedValue);
                    artist.GroupingId = grouping.Id;
                    break;
                case ItemProperty.State:
                    artist.State = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _updateService.UpdateArtist(artist);
    }

    public async Task UpdateTrack(ItemUpdateRequestDTO request)
    {
        var track = await _readService.GetTrack(request.Id);

        foreach (var update in request.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.Lyrics:
                    track.Lyrics = update.UpdatedValue;
                    break;
                case ItemProperty.TrackTags:
                    track.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.TrackTitle:
                    track.Title = update.UpdatedValue;
                    break;
                case ItemProperty.TrackYear:
                    track.Year = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _updateService.UpdateTrack(track);
    }
}
