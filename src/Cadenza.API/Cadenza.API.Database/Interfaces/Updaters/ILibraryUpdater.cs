namespace Cadenza.API.Database.Interfaces.Updaters;

internal interface ILibraryUpdater
{
    void AddTrack(FullLibrary library, TrackFull track);
    void RemoveTracks(FullLibrary library, List<string> trackIds);
}
