using Cadenza.Domain;

namespace Cadenza.Local.Common.Interfaces;

public interface IUpdateHistory
{
    Task<DateTime> GetDateLastUpdated(LibrarySource source);

    Task UpdateDateLastUpdated(DateTime date, LibrarySource source);
}
