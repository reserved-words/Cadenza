namespace Cadenza.Utilities.Interfaces;

public interface ILogger
{
    Task LogError(Exception ex);
    Task LogError(string message);
    Task LogInfo(string message);
}
