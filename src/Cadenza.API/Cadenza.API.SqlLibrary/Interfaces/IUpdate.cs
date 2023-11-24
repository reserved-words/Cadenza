using Cadenza.Database.SqlLibrary.Model.Update;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IUpdate
{
    Task<int> AddArtist(AddArtistParameter data);
    Task<int> AddAlbum(AddAlbumParameter data);
    Task<int> AddDisc(AddDiscParameter data);
    Task<int> AddTrack(AddTrackParameter data);

    Task DeleteTrack(int id);
    Task DeleteEmptyDiscs();
    Task DeleteEmptyAlbums();
    Task DeleteEmptyArtists();
    Task DeleteTrack(string idFromSource);

    Task UpdateAlbum(UpdateAlbumParameter album);
    Task UpdateArtist(UpdateArtistParameter artist);
    Task UpdateTrack(UpdateTrackParameter track);
}
