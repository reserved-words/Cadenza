using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;

namespace Cadenza.Local.API.Common.Interfaces;

public interface IMusicFileLibrary
{
    TrackFull GetFileData(string filepath);
    void UpdateFileData(string filepath, List<PropertyUpdate> updates);
}
