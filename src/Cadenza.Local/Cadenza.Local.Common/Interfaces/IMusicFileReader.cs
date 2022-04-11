using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Common.Interfaces;

public interface IMusicFileArtworkService
{
    (byte[] Bytes, string Type) GetArtwork(string filepath);
}
