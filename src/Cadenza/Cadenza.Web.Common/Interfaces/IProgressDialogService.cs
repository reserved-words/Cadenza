namespace Cadenza.Web.Common.Interfaces;

public interface IProgressDialogService
{
    Task<bool> Run(Func<List<SubTask>> taskFactory);
}
