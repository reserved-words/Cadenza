using Cadenza.Domain.Models.Updates;

namespace Cadenza.Local.API.Core.Interfaces;

internal interface ILocalFilesUpdater
{
    Task UpdateArtist(string id, List<PropertyUpdate> updates);
    Task UpdateAlbum(string id, List<PropertyUpdate> updates);
    Task UpdateTrack(string id, List<PropertyUpdate> updates);
}
