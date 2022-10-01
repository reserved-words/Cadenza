namespace Cadenza.API.Database.Services.Updaters;

internal class ItemUpdater : IItemUpdater
{
    public void UpdateAlbum(JsonAlbum album, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
                case ItemProperty.AlbumTitle:
                    album.Title = update.UpdatedValue;
                    break;
                case ItemProperty.ReleaseType:
                    album.ReleaseType = update.UpdatedValue;
                    break;
                case ItemProperty.ReleaseYear:
                    album.Year = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public void UpdateTrack(JsonTrack track, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
                case ItemProperty.Lyrics:
                    track.Lyrics = update.UpdatedValue;
                    break;
                case ItemProperty.TrackTitle:
                    track.Title = update.UpdatedValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public void UpdateArtist(JsonArtist artist, List<PropertyUpdate> updates)
    {
        foreach (var update in updates)
        {
            switch (update.Property)
            {
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
                    artist.Grouping = update.UpdatedValue;
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
