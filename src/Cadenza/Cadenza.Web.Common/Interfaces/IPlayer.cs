namespace Cadenza.Web.Common.Interfaces;
public interface IPlayer
{
    Task Play();
    Task<int> Pause();
    Task<int> Resume();
    Task<int> Stop();
}