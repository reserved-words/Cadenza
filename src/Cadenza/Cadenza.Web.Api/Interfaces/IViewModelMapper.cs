﻿namespace Cadenza.Web.Api.Interfaces;

internal interface IViewModelMapper
{
    // AlbumDetailsVM Map(AlbumDetailsDTO dto);
    AlbumFullVM Map(AlbumFullDTO dto);
    AlbumVM Map(AlbumDTO dto);
    ArtistVM Map(ArtistDTO dto);
    ArtistFullVM Map(ArtistFullDTO dto);
    GroupingVM Map(GroupingDTO dto);
    RecentAlbumVM Map(RecentAlbumDTO dto);
    SearchItemVM Map(SearchItemDTO dto);
    TaggedItemVM Map(TaggedItemDTO dto);
    TrackDetailsVM Map(TrackDetailsDTO track);
    TrackFullVM Map(TrackFullDTO track);
}
