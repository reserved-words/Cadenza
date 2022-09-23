using Cadenza.Domain;
using Cadenza.Local.Common.Model;

namespace Cadenza.Local.Common.Interfaces;

public interface IMusicFileLibrary
{
    LocalFileData GetFileData(string filepath);
    void UpdateFileData(string filepath, List<ItemPropertyUpdate> updates);
}
