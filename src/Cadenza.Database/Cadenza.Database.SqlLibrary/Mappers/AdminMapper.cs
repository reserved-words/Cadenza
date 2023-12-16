using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Admin;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class AdminMapper : IAdminMapper
{
    public GroupingDTO MapGrouping(GetGroupingsResult result)
    {
        return new GroupingDTO 
        {
            Id = result.Id, 
            Name = result.Name,
            IsUsed = result.IsUsed
        };
    }
}
