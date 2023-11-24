using Cadenza.Database.SqlLibrary.Model.Queue;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IQueueMapper
{
    AddArtistUpdateParameter MapArtistUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update);
    AddAlbumUpdateParameter MapAlbumUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update);
    AddTrackUpdateParameter MapTrackUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update);

    List<ItemUpdateRequestDTO> MapArtistUpdateRequests(List<GetArtistUpdatesResult> data);
    List<ItemUpdateRequestDTO> MapAlbumUpdateRequests(List<GetAlbumUpdatesResult> data);
    List<ItemUpdateRequestDTO> MapTrackUpdateRequests(List<GetTrackUpdatesResult> data);

    SyncTrackRemovalRequestDTO MapSyncTrackRemovalRequest(GetTrackRemovalsResult data);
}
