namespace Cadenza.Library;

public interface ILibraryUpdater
{
    Task<bool> Update(AlbumInfo album, List<ItemPropertyUpdate> updates);
    Task<bool> Update(ArtistInfo artist, List<ItemPropertyUpdate> updates);
    Task<bool> Update(TrackInfo track, List<ItemPropertyUpdate> updates);
}