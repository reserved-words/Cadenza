﻿namespace Cadenza.Web.Common.Interfaces;

public interface IAdminRepository
{
    Task<List<Grouping>> GetGroupingOptions();
}