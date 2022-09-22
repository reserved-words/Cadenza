using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces;

public interface IUpdateHistory
{
    Task<DateTime> GetDateLastUpdated(LibrarySource source);

    Task UpdateDateLastUpdated(DateTime date, LibrarySource source);
}
