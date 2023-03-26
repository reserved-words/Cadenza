using Cadenza.API.SqlLibrary.Model;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.API.SqlLibrary.Interfaces;
internal interface IDataMapper
{
    AddArtistData MapTrackArtist(TrackFull track);
    AddArtistData MapAlbumArtist(TrackFull track);
    AddAlbumData MapAlbum(TrackFull track, int artistId);
    AddDiscData MapDisc(TrackFull track, int albumId);
    AddTrackData MapTrack(TrackFull track, int artistId, int discId);
}
