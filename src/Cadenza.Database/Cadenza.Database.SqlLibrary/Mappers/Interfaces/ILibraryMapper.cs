using Cadenza.Database.SqlLibrary.Model.Library;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface ILibraryMapper
{
    ArtistDTO MapArtist(GetArtistsByGroupingResult artist);
    ArtistDTO MapArtist(GetArtistsByGenreResult artist);
    AlbumDTO MapAlbum(GetArtistAlbumsResult album);
    AlbumDTO MapAlbum(GetAlbumsFeaturingArtistResult album);
    TaggedItemDTO MapTaggedItem(GetTaggedItemsResult result);
    ArtistDetailsDTO MapArtist(GetArtistResult artist);
    AlbumDetailsDTO MapAlbum(GetAlbumResult album);
    AlbumFullDTO MapAlbum(GetFullAlbumResult album);
    TrackDetailsDTO MapTrack(GetTrackResult track);
    TrackFullDTO MapTrack(GetFullTrackResult track);
    List<AlbumDiscDTO> MapAlbumTracks(int id, List<GetAlbumDiscsResult> discs, List<GetAlbumTracksResult> tracks);
}
