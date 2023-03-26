using Cadenza.API.Interfaces.Repositories;
using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Updates;

namespace Cadenza.API.SqlLibrary;

internal class UpdateRepository : IUpdateRepository
{
    public Task Add(ItemUpdates update, LibrarySource? itemSource)
    {
        throw new NotImplementedException();
    }

    public Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        throw new NotImplementedException();
    }

    public Task Remove(ItemUpdates update, LibrarySource source)
    {
        throw new NotImplementedException();
    }
}
