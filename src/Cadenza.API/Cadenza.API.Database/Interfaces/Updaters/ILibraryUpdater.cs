using Cadenza.Domain.Model.Track;

namespace Cadenza.API.Database.Interfaces.Updaters;

internal interface ILibraryUpdater
{
    void AddTrack(JsonItems library, TrackFull track);
    void RemoveTracks(JsonItems library, List<string> trackIds);
}
