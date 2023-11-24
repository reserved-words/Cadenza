﻿using Cadenza.Database.SqlLibrary.Model.Admin;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class AdminMapper : IAdminMapper
{
    public GroupingDTO MapGrouping(GetGroupingsResult result)
    {
        return new GroupingDTO(result.Id, result.Name);
    }
}
