namespace Cadenza.Web.Database.Interfaces;

internal interface IViewModelMapper
{
    AlbumDetailsVM Map(AlbumDetailsDTO dto);
    AlbumTracksVM Map(AlbumTracksDTO dto);
    AlbumVM Map(AlbumDTO dto);
    ArtistVM Map(ArtistDTO dto);
    ArtistDetailsVM Map(ArtistDetailsDTO dto);
    TrackVM Map(TrackDTO dto);
    PlayerItemVM Map(SearchItemDTO dto);
    TrackFullVM Map(TrackFullDTO dto);
    GroupingVM Map(GroupingDTO dto);
    RecentAlbumVM Map(RecentAlbumDTO dto);
}
