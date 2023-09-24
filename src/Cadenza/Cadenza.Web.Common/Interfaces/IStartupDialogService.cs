namespace Cadenza.Web.Common.Interfaces;

public interface IStartupDialogService
{
    Task<bool> Run();
}
