using Cadenza.Database.SqlLibrary.Model.Admin;

namespace Cadenza.Database.SqlLibrary.Interfaces;
internal interface IAdminMapper
{
    GroupingDTO MapGrouping(GetGroupingsResult result);
}
