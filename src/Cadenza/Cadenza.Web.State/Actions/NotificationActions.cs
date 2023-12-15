namespace Cadenza.Web.State.Actions;

public record NotificationInformationRequest(string Message);
public record NotificationSuccessRequest(string Message);
public record NotificationErrorRequest(string Message, string Error, string StackTrace);