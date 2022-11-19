namespace Cadenza.API.Interfaces.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task RemoveTracks(LibrarySource source, List<string> id);
    Task UpdateArtist(EditedItem updates);
    Task UpdateAlbum(LibrarySource source, EditedItem updates);
    Task UpdateTrack(LibrarySource source, EditedItem updates);
    Task AddTrack(LibrarySource source, TrackFull track);
}
