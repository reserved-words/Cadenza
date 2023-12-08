namespace Cadenza.Web.Api.Interfaces;

internal interface IViewModelMapper
{
    AlbumDetailsVM Map(AlbumDetailsDTO dto);
    AlbumDiscVM Map(AlbumDiscDTO dto);
    AlbumVM Map(AlbumDTO dto);
    ArtistVM Map(ArtistDTO dto);
    ArtistDetailsVM Map(ArtistDetailsDTO dto);
    SearchItemVM Map(SearchItemDTO dto);
    TaggedItemVM Map(TaggedItemDTO dto);
    TrackFullVM Map(TrackFullDTO dto);
    GroupingVM Map(GroupingDTO dto);
    RecentAlbumVM Map(RecentAlbumDTO dto);
}
