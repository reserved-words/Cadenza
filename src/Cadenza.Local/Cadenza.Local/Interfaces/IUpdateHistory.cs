namespace Cadenza.Local;

public interface IUpdateHistory
{
    Task<DateTime> GetDateProcessedModifiedFiles();

    Task SetDateProcessedModifiedFiles(DateTime date);
}