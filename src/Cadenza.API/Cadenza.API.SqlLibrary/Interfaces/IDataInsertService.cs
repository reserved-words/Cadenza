using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDataInsertService
{
    Task<int> AddArtist(NewArtistData data);
    Task<int> AddAlbum(NewAlbumData data);
    Task<int> AddDisc(NewDiscData data);
    Task<int> AddTrack(NewTrackData data);
}
