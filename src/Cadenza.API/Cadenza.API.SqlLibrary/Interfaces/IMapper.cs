namespace Cadenza.Database.SqlLibrary.Interfaces;
internal interface IMapper
{
    NewArtistData MapTrackArtist(SyncTrackDTO track);
    NewArtistData MapAlbumArtist(SyncTrackDTO track);
    NewAlbumData MapAlbum(SyncTrackDTO track, LibrarySource source, int artistId);
    NewDiscData MapDisc(SyncTrackDTO track, int albumId);
    NewTrackData MapTrack(SyncTrackDTO track, int artistId, int discId);

    ArtistDetailsDTO MapArtist(GetArtistData artist);
    AlbumDetailsDTO MapAlbum(GetAlbumData album, List<GetDiscData> discs);
    AlbumTrackLinkDTO MapAlbumTrack(GetTrackData track);
    TrackDetailsDTO MapTrack(GetTrackData track);

    RecentAlbumDTO MapRecentAlbum(RecentAlbumData data);

    SyncTrackRemovalRequestDTO MapSyncTrackRemovalRequest(TrackRemovalData data);

    NewArtistUpdateData MapArtistUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update);
    NewAlbumUpdateData MapAlbumUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update);
    NewTrackUpdateData MapTrackUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update);

    List<ItemUpdateRequestDTO> MapArtistUpdateRequests(List<ArtistUpdateData> data);
    List<ItemUpdateRequestDTO> MapAlbumUpdateRequests(List<AlbumUpdateData> data);
    List<ItemUpdateRequestDTO> MapTrackUpdateRequests(List<TrackUpdateData> data);
}
