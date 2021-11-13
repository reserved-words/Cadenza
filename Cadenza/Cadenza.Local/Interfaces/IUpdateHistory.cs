namespace Cadenza.Local;

public interface IUpdateHistory
{
    DateTime GetDateProcessedModifiedFiles();

    void SetDateProcessedModifiedFiles(DateTime date);
}