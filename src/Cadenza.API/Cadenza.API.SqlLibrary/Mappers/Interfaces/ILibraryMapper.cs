using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface ILibraryMapper
{
    ArtistDetailsDTO MapArtist(GetArtistsResult artist);
    AlbumDetailsDTO MapAlbum(GetAlbumsResult album, List<GetDiscsResult> discs);
    AlbumDTO MapAlbum(GetArtistAlbumsResult album);
    AlbumDTO MapAlbum(GetAlbumsFeaturingArtistResult album);
    AlbumTrackLinkDTO MapAlbumTrack(GetTracksResult track);
    TrackDetailsDTO MapTrack(GetTracksResult track);
    TaggedItemDTO MapTaggedItem(GetTaggedItemsResult result);
    ArtistDetailsDTO MapArtist(GetArtistResult artist);
    AlbumDetailsDTO MapAlbum(GetAlbumResult album, List<GetAlbumDiscsResult> discs);
    TrackFullDTO MapTrack(GetTrackResult track);
    AlbumTracksDTO MapAlbumTracks(int id, List<GetAlbumDiscsResult> discs, List<GetAlbumTracksResult> tracks);
}
