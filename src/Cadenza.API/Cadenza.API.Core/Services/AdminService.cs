﻿namespace Cadenza.API.Core.Services;

internal class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;

    public AdminService(IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Grouping>> GetGroupings()
    {
        return await _repository.GetGroupings();
    }

}