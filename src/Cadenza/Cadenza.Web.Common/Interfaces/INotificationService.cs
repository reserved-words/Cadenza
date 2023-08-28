namespace Cadenza.Web.Common.Interfaces;

public interface INotificationService
{
    void Success(string message);
    void Error(string message);
}