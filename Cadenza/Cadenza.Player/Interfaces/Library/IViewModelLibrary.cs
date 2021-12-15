namespace Cadenza.Player;

public interface IViewModelLibrary
{
    event AlbumUpdatedEventHandler AlbumUpdated;
    event ArtistUpdatedEventHandler ArtistUpdated;
    event TrackUpdatedEventHandler TrackUpdated;

}