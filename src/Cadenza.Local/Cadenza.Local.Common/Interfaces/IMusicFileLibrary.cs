using Cadenza.Domain;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Common.Interfaces;

public interface IMusicFileLibrary
{
    JsonFileData GetFileData(string filepath);
    void UpdateFileData(string filepath, List<ItemPropertyUpdate> updates);
}
