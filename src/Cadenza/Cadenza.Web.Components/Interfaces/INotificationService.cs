namespace Cadenza.Web.Components.Interfaces;

public interface INotificationService
{
    void Success(string message);
    void Error(string message);
}