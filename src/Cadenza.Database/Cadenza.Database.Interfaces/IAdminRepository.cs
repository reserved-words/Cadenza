namespace Cadenza.Database.Interfaces;

public interface IAdminRepository
{
    Task<bool> HasLastFmSessionKey();
    Task SaveLastFmSessionKey(string lastFmUsername, string lastFmSessionKey);
}
