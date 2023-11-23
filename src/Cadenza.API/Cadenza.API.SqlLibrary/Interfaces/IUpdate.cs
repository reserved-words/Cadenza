namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IUpdate
{
    Task<int> AddArtist(NewArtistData data);
    Task<int> AddAlbum(NewAlbumData data);
    Task<int> AddDisc(NewDiscData data);
    Task<int> AddTrack(NewTrackData data);

    Task DeleteTrack(int id);
    Task DeleteEmptyDiscs();
    Task DeleteEmptyAlbums();
    Task DeleteEmptyArtists();
    Task DeleteTrack(string idFromSource);

    Task UpdateAlbum(AlbumData album);
    Task UpdateArtist(ArtistData artist);
    Task UpdateTrack(TrackData track);
}
