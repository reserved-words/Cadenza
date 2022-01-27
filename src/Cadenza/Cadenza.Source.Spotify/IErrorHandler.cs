namespace Cadenza.Source.Spotify;

public interface IErrorHandler
{
    Task<bool> DeviceNotFound();
    Task UnexpectedError();
}
