namespace Cadenza
{
    public class PlayerApiConfig : IPlayerApiUrl
    {
        private readonly IConfigurationSection _config;

        public PlayerApiConfig(IConfiguration config)
        {
            _config = config.GetSection("PlayerApi");
        }

        public string Scrobble => GetEndpoint("Scrobble");

        public string UpdateNowPlaying => GetEndpoint("UpdateNowPlaying");

        public string IsFavourite => GetEndpoint("IsFavourite");

        public string Favourite => GetEndpoint("Favourite");

        public string Unfavourite => GetEndpoint("Unfavourite");

        private string GetEndpoint(string name)
        {
            var baseUrl = _config.GetValue<string>("BaseUrl");
            var endpoint = _config.GetSection("Endpoints").GetValue<string>(name);
            return $"{baseUrl}{endpoint}";
        }
    }
}
