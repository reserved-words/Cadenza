namespace Cadenza.API.Core.Interfaces;

internal interface ICachePopulater
{
    Task Populate(bool onlyIfEmpty);
}
