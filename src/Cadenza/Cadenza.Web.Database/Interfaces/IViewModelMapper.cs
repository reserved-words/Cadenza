namespace Cadenza.Web.Database.Interfaces;

internal interface IViewModelMapper
{
    AlbumDetailsVM Map(AlbumDetailsDTO dto);
    AlbumTrackVM Map(AlbumTrackDTO dto);
    AlbumVM Map(AlbumDTO dto);
    ArtistVM Map(ArtistDTO dto);
    ArtistDetailsVM Map(ArtistDetailsDTO dto);
    TrackVM Map(TrackDTO dto);
    PlayerItemVM Map(PlayerItemDTO dto);
    TrackFullVM Map(TrackFullDTO dto);
    GroupingVM Map(GroupingDTO dto);
}
