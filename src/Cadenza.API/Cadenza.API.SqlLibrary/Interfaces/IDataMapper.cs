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

    ArtistDetails MapArtist(GetArtistData artist);
    AlbumDetails MapAlbum(GetAlbumData album, List<GetDiscData> discs);
    AlbumTrackLink MapAlbumTrack(GetTrackData track);
    TrackDetails MapTrack(GetTrackData track);
}
