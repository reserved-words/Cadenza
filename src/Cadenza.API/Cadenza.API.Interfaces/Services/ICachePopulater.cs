namespace Cadenza.API.Interfaces.Services;

public interface ICachePopulater
{
    Task Populate(bool onlyIfEmpty);
}
