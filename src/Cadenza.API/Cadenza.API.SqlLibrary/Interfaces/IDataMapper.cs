using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Interfaces;
internal interface IDataMapper
{
    NewArtistData MapTrackArtist(TrackFull track);
    NewArtistData MapAlbumArtist(TrackFull track);
    NewAlbumData MapAlbum(TrackFull track, int artistId);
    NewDiscData MapDisc(TrackFull track, int albumId);
    NewTrackData MapTrack(TrackFull track, int artistId, int discId);

    ArtistInfo MapArtist(GetArtistData artist);
    AlbumInfo MapAlbum(GetAlbumData album, List<GetDiscData> discs);
    AlbumTrackLink MapAlbumTrack(GetTrackData track);
    TrackInfo MapTrack(GetTrackData track);
}
