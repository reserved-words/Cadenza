namespace Cadenza.Database.SqlLibrary.Database.Interfaces;

internal interface IAdmin
{
    Task<bool> HasLastFmSessionKey();
    Task SaveLastFmSessionKey(string lastFmUsername, string lastFmSessionKey);
}