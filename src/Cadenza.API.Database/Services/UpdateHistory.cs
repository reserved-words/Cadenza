using Cadenza.API.Common.Interfaces;
using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Database.Services;

public class UpdateHistory : IUpdateHistory
{
    private readonly IDataAccess _dataAccess;

    public UpdateHistory(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<DateTime> GetDateLastUpdated(LibrarySource source)
    {
        var history = await _dataAccess.GetUpdateHistory(source);
        return history.ModifiedFilesLastUpdated;
    }

    public async Task UpdateDateLastUpdated(DateTime date, LibrarySource source)
    {
        var history = new JsonUpdateHistory { ModifiedFilesLastUpdated = date };
        await _dataAccess.SaveUpdateHistory(history, source);
    }
}
