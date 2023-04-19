namespace Cadenza.Web.Common.Interfaces.Startup;

public interface IStartupService
{
    Task<bool> Startup();
}
