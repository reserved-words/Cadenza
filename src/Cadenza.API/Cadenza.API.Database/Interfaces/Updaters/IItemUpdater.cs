using Cadenza.Domain.Model.Updates;

namespace Cadenza.API.Database.Interfaces.Updaters;

internal interface IItemUpdater
{
    void UpdateAlbum(JsonAlbum album, List<PropertyUpdate> updates);
    void UpdateArtist(JsonArtist artist, List<PropertyUpdate> updates);
    void UpdateTrack(JsonTrack track, List<PropertyUpdate> updates);
}
