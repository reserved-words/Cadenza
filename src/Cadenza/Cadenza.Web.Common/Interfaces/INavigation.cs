namespace Cadenza.Web.Common.Interfaces;

public interface INavigation
{
    Task OpenNewTab(string url);
}