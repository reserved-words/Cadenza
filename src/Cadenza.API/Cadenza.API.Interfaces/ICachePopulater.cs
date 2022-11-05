namespace Cadenza.API.Interfaces;

public interface ICachePopulater
{
    Task Populate(bool onlyIfEmpty);
}
