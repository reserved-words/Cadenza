using Cadenza.API.SqlLibrary.Model;
using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.API.SqlLibrary.Interfaces;
internal interface IDataMapper
{
    NewArtistData MapTrackArtist(SyncTrack track);
    NewArtistData MapAlbumArtist(SyncTrack track);
    NewAlbumData MapAlbum(SyncTrack track, LibrarySource source, int artistId);
    NewDiscData MapDisc(SyncTrack track, int albumId);
    NewTrackData MapTrack(SyncTrack track, int artistId, int discId);

    ArtistInfo MapArtist(GetArtistData artist);
    AlbumInfo MapAlbum(GetAlbumData album, List<GetDiscData> discs);
    AlbumTrackLink MapAlbumTrack(GetTrackData track);
    TrackInfo MapTrack(GetTrackData track);
}
