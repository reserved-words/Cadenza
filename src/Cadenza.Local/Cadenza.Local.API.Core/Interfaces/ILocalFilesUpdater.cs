using Cadenza.Common.DTO;

namespace Cadenza.Local.API.Core.Interfaces;

internal interface ILocalFilesUpdater
{
    Task UpdateArtist(string id, List<PropertyUpdateDTO> updates);
    Task UpdateAlbum(string id, List<PropertyUpdateDTO> updates);
    Task UpdateTrack(string id, List<PropertyUpdateDTO> updates);
}
