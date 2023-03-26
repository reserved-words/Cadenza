using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IInsertService
{
    Task<int> AddArtist(AddArtistData data);
    Task<int> AddAlbum(AddAlbumData data);
    Task<int> AddDisc(AddDiscData data);
    Task<int> AddTrack(AddTrackData data);
}
