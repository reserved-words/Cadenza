using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Interfaces;
internal interface IDataMapper
{
    AddArtistData MapTrackArtist(TrackFull track);
    AddArtistData MapAlbumArtist(TrackFull track);
    AddAlbumData MapAlbum(TrackFull track, int artistId);
    AddDiscData MapDisc(TrackFull track, int albumId);
    AddTrackData MapTrack(TrackFull track, int artistId, int discId);

    ArtistInfo MapArtist(GetArtistData artist);
    AlbumInfo MapAlbum(GetAlbumData album, List<GetDiscData> discs);
    AlbumTrackLink MapAlbumTrack(GetTrackData track);
    TrackInfo MapTrack(GetTrackData track);
}
