namespace Cadenza.Database.Interfaces;

public interface IAdminRepository
{
    Task<List<GroupingDTO>> GetGroupings();
    Task<bool> HasLastFmSessionKey(string username);
    Task SaveLastFmSessionKey(string username, string lastFmUsername, string lastFmSessionKey);
}
