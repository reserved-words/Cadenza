namespace Cadenza.Local.API.Common.Interfaces;

public interface IArtworkFilesService
{
    (byte[] Bytes, string Type) GetArtwork(string filepath);
}
