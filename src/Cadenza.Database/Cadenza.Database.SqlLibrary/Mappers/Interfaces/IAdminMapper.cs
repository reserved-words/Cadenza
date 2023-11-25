using Cadenza.Database.SqlLibrary.Model.Admin;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;
internal interface IAdminMapper
{
    GroupingDTO MapGrouping(GetGroupingsResult result);
}
