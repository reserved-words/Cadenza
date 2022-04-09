namespace Cadenza.Local;

public interface IUpdateHistory
{
    Task<DateTime> GetDateLastUpdated(LibrarySource source);

    Task UpdateDateLastUpdated(DateTime date, LibrarySource source);
}