using Cadenza.Domain.Model.Update;

namespace Cadenza.Web.Common.Events;

public delegate Task ArtistUpdatedEventHandler(object sender, ArtistUpdatedEventArgs e);

public class ArtistUpdatedEventArgs : EventArgs
{
    public ArtistUpdatedEventArgs(ArtistUpdate update)
    {
        Update = update;
    }

    public ArtistUpdate Update { get; }
}
