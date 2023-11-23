namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IAdmin
{
    Task<List<GroupingDTO>> GetGroupings();
}