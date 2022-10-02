namespace Cadenza.Web.Common.Interfaces.Searchbar;

public interface ISearchMessenger
{
    event EventHandler UpdateCompleted;
    event EventHandler UpdateStarted;
}
