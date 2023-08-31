using Cadenza.Common.Domain.Model.Sync;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IMusicFilesService
{
    Task<SyncTrack> GetFileData(string id, string filepath);
    Task UpdateFileData(string filepath, List<PropertyUpdate> updates);
}
