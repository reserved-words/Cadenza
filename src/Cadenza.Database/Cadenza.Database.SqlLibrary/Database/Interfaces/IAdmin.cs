namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IAdmin
{
    Task<bool> HasLastFmSessionKey(string username);
    Task SaveLastFmSessionKey(string username, string lastFmUsername, string lastFmSessionKey);
}