using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDataUpdateService
{
    Task UpdateAlbum(AlbumData album);
    Task UpdateArtist(ArtistData artist);
    Task UpdateTrack(TrackData track);
}