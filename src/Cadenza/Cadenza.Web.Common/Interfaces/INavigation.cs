namespace Cadenza.Web.Common.Interfaces;

public interface INavigation
{
    Task NavigateTo(string url);
    Task OpenNewTab(string url);
}