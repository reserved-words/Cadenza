namespace Cadenza.Local;

public class UpdateHistory : IUpdateHistory
{
    private readonly IDataAccess _dataAccess;

    public UpdateHistory(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public DateTime GetDateProcessedModifiedFiles()
    {
        var history = _dataAccess.GetUpdateHistory();
        return history.ModifiedFilesLastUpdated;
    }

    public void SetDateProcessedModifiedFiles(DateTime date)
    {
        var history = new JsonUpdateHistory { ModifiedFilesLastUpdated = date };
        _dataAccess.SaveUpdateHistory(history);
    }
}
