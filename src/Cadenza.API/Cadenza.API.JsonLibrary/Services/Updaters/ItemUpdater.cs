using Cadenza.API.JsonLibrary.Interfaces.Updaters;

namespace Cadenza.API.JsonLibrary.Services.Updaters;

internal class ItemUpdater : IItemUpdater
{
    public void UpdateAlbum(AlbumInfo album, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
                case ItemProperty.ReleaseType:
                    album.ReleaseType = Enum.Parse<ReleaseType>(update.UpdatedValue);
                    break;
                case ItemProperty.AlbumTags:
                    album.Tags = new TagList(update.UpdatedValue);
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
    }

    public void UpdateTrack(TrackInfo track, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
                case ItemProperty.Lyrics:
                    track.Lyrics = update.UpdatedValue;
                    break;
                case ItemProperty.TrackTags:
                    track.Tags = new TagList(update.UpdatedValue);
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
    }

    public void UpdateArtist(ArtistInfo artist, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
                case ItemProperty.ArtistImage:
                    artist.ImageUrl = update.UpdatedValue;
                    break;
                case ItemProperty.ArtistTags:
                    artist.Tags = new TagList(update.UpdatedValue);
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
                    artist.Grouping = Enum.Parse<Grouping>(update.UpdatedValue);
                    break;
                case ItemProperty.State:
                    artist.State = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
