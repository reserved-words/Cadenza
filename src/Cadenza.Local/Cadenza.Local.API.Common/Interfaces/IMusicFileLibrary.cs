using Cadenza.Domain.Models.Track;
using Cadenza.Domain.Models.Updates;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IMusicFileLibrary
{
    TrackFull GetFileData(string filepath);
    void UpdateFileData(string filepath, List<PropertyUpdate> updates);
}
