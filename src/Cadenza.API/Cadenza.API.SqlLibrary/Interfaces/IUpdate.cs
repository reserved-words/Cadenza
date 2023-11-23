namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IUpdate
{
    Task DeleteTrackById(int id);
    Task DeleteEmptyDiscs();
    Task DeleteEmptyAlbums();
    Task DeleteEmptyArtists();
    Task DeleteTrackByIdFromSource(string idFromSource);

    Task UpdateAlbum(AlbumData album);
    Task UpdateArtist(ArtistData artist);
    Task UpdateTrack(TrackData track);
}
