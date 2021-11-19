namespace Cadenza;

public interface INotificationService
{
    void Success(string message);
    void Error(string message);
}