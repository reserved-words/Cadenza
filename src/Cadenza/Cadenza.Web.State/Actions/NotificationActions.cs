namespace Cadenza.Web.State.Actions;

public record NotificationSuccessRequest(string Message);
public record NotificationErrorRequest(string Message, string Error, string StackTrace);