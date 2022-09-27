using Cadenza.Domain.Model.Track;
using Cadenza.Domain.Model.Updates;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IMusicFileLibrary
{
    TrackFull GetFileData(string filepath);
    void UpdateFileData(string filepath, List<PropertyUpdate> updates);
}
