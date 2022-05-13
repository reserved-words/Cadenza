using Cadenza.Source.Spotify;
using Cadenza.Source.Spotify.Model;

namespace Cadenza.UI.Tabs.Spotify
{
    public class SpotifyAlbumTabBase : ComponentBase
    {
        [Inject]
        public ISpotifyLibrary Library { get; set; }

        [Parameter]
        public SpotifyAlbumSearchResult Model { get; set; }

        protected async Task AddAlbum(string id)
        {
            await Library.AddAlbum(id);
        }
    }
}
