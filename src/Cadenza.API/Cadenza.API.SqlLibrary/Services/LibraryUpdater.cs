using Cadenza.API.SqlLibrary.Interfaces;

namespace Cadenza.API.SqlLibrary.Services;

internal class LibraryUpdater : ILibraryUpdater
{
    private readonly IDataReadService _readService;
    private readonly IDataUpdateService _updateService;

    public LibraryUpdater(IDataUpdateService updateService, IDataReadService readService)
    {
        _updateService = updateService;
        _readService = readService;
    }

    public async Task UpdateAlbum(ItemUpdates updates)
    {
        var album = await _readService.GetAlbum(int.Parse(updates.Id));

        foreach (var update in updates.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.ReleaseType:
                    album.ReleaseTypeId = (int)Enum.Parse<ReleaseType>(update.UpdatedValue);
                    break;
                case ItemProperty.AlbumTags:
                    album.TagList = update.UpdatedValue;
                    break;
                case ItemProperty.ReleaseYear:
                    album.Year = update.UpdatedValue;
                    break;
                case ItemProperty.Artwork:
                    album.ArtworkUrl = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        await _updateService.UpdateAlbum(album);
    }

    public async Task UpdateArtist(ItemUpdates updates)
    {
        var artist = await _readService.GetArtist(updates.Id);

        foreach (var update in updates.Updates)
        {
            switch (update.Property)
            {
                case ItemProperty.ArtistImage:
                    artist.ImageUrl = update.UpdatedValue;
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
                    artist.GroupingId = (int)Enum.Parse<Grouping>(update.UpdatedValue);
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

    public async Task UpdateTrack(ItemUpdates updates)
    {
        var track = await _readService.GetTrack(updates.Id);

        foreach (var update in updates.Updates)
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
