namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ITrackAdder
{
    Task AddTrack(LibrarySource source, SyncTrackDTO track);
}
