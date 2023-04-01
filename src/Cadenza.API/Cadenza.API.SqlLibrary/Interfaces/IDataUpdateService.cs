using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDataUpdateService
{
    Task UpdateAlbum(AlbumData album);
    Task UpdateArtist(ArtistData artist);
    Task UpdateTrack(TrackData track);
    Task MarkAlbumUpdateDone(int id);
    Task MarkArtistUpdateDone(int id);
    Task MarkTrackUpdateDone(int id);
    Task MarkAlbumUpdateErrored(int id);
    Task MarkArtistUpdateErrored(int id);
    Task MarkTrackUpdateErrored(int id);
}