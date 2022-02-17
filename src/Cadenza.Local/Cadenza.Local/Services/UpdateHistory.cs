namespace Cadenza.Local;

public class UpdateHistory : IUpdateHistory
{
    private readonly IDataAccess _dataAccess;

    public UpdateHistory(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<DateTime> GetDateProcessedModifiedFiles()
    {
        var history = await _dataAccess.GetUpdateHistory();
        return history.ModifiedFilesLastUpdated;
    }

    public async Task SetDateProcessedModifiedFiles(DateTime date)
    {
        var history = new JsonUpdateHistory { ModifiedFilesLastUpdated = date };
        await _dataAccess.SaveUpdateHistory(history);
    }
}
