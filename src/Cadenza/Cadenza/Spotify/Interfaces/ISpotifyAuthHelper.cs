namespace Cadenza
{
    public interface ISpotifyAuthHelper
    {
        Task<string> CreateSession(CancellationToken cancellationToken);
        Task<string> GetAccessToken(CancellationToken cancellationToken);
    }
}
