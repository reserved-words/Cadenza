using Cadenza.Common.Domain.Model.Track;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IMusicFilesService
{
    TrackFull GetFileData(string filepath);
    void UpdateFileData(string filepath, List<PropertyUpdate> updates);
}
