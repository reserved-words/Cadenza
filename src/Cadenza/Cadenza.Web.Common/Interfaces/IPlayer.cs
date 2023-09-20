namespace Cadenza.Web.Common.Interfaces;
public interface IPlayer
{
    Task Play(Track track);
    Task<int> Pause(Track track);
    Task<int> Resume(Track track);
    Task<int> Stop(Track track);
}