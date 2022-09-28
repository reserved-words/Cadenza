using Cadenza.Web.Core.Events;

namespace Cadenza.Web.Core.Updates;

public interface ILibraryConsumer
{
    //event AlbumUpdatedEventHandler AlbumUpdated;
    event ArtistUpdatedEventHandler ArtistUpdated;
    //event TrackUpdatedEventHandler TrackUpdated;

}