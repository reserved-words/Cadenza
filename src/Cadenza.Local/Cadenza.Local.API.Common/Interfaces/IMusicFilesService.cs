using Cadenza.Common.DTO;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IMusicFilesService
{
    Task<SyncTrackDTO> GetFileData(string id, string filepath);
    Task UpdateFileData(string filepath, List<PropertyUpdateDTO> updates);
}
