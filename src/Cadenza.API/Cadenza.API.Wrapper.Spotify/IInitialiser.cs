namespace Cadenza.API.Wrapper.Spotify
{
    public interface IInitialiser
    {
        Task Populate(string accessToken);
    }
}