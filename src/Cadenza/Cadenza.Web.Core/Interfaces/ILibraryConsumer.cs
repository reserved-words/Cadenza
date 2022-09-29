using Cadenza.Web.Common.Events;

namespace Cadenza.Web.Core.Interfaces;

internal interface ILibraryConsumer
{
    event ArtistUpdatedEventHandler ArtistUpdated;

}