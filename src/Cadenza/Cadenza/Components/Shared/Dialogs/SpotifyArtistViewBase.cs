namespace Cadenza.Components.Shared.Dialogs
{
    public class SpotifyArtistViewBase : ViewBase<SpotifyArtistProfile>
    {
        [Inject]
        public ISpotifyLibrary Library { get; set; }

        protected async Task AddAlbum(string id)
        {
            await Library.AddAlbum(id);
        }
        protected async Task AddPlaylist(string id)
        {
            await Library.AddPlaylist(id);
        }
    }
}
