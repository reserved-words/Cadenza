namespace Cadenza.Local.API.Common.Interfaces;

public interface IWebImageService
{
    Task<byte[]> GetBytes(string url);
}