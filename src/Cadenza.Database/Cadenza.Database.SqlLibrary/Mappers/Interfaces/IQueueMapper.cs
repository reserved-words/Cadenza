using Cadenza.Database.SqlLibrary.Model.Queue;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface IQueueMapper
{
    List<ArtistUpdateSyncDTO> MapArtistUpdates(List<GetArtistUpdatesResult> data);
    List<AlbumUpdateSyncDTO> MapAlbumUpdates(List<GetAlbumUpdatesResult> data);
    List<TrackUpdateSyncDTO> MapTrackUpdates(List<GetTrackUpdatesResult> data);

    TrackRemovalSyncDTO MapSyncTrackRemovalRequest(GetTrackRemovalsResult data);
}
