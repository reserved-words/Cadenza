namespace Cadenza.Source.Spotify.Interfaces
{
    public interface IInitialiser
    {
        Task Populate(string accessToken);
    }
}