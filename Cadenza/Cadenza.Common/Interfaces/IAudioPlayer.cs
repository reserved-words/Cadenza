namespace Cadenza.Common;

public interface IAudioPlayer
{
    Task Play(string id);
    Task<int> Pause();
    Task<int> Resume();
    Task<int> Stop();
}
