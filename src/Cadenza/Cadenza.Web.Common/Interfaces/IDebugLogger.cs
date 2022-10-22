namespace Cadenza.Web.Common.Interfaces;

public interface IDebugLogger
{
    Task LogError(Exception ex);
    Task LogInfo(string message);
    Task DisplayInfo(string message);
}
