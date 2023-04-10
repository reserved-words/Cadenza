namespace Cadenza.Web.Common.Interfaces;

public interface IHistoryLogger
{
    Task LogPlayedItem(PlaylistId playlistId);
}
