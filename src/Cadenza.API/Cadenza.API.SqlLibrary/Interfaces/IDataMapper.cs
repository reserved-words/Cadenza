using Cadenza.API.SqlLibrary.Model;
using Cadenza.Common.Enums;

namespace Cadenza.API.SqlLibrary.Interfaces;
internal interface IDataMapper
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
}
