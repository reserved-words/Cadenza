using Cadenza.Database.SqlLibrary.Model.Admin;

namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IAdmin
{
    Task<List<GetGroupingsResult>> GetGroupings();
    Task SaveLastFmSessionKey(string username, string lastFmUsername, string lastFmSessionKey);
}