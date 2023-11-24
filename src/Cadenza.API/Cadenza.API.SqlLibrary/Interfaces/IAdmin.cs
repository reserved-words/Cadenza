using Cadenza.Database.SqlLibrary.Model.Admin;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IAdmin
{
    Task<List<GetGroupingsResult>> GetGroupings();
}