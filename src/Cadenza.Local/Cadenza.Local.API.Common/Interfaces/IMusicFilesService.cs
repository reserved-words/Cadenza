using Cadenza.Common.Domain.Model.Track;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IMusicFilesService
{
    Task<TrackFull> GetFileData(string filepath);
    Task UpdateFileData(string filepath, List<EditedProperty> updates);
}
