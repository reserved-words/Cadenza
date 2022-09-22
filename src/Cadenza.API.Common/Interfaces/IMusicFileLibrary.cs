using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces;

public interface IMusicFileLibrary
{
    JsonFileData GetFileData(string filepath);
    void UpdateFileData(string filepath, List<ItemPropertyUpdate> updates);
}
