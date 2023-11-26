using Cadenza.SyncService.Model;

namespace Cadenza.SyncService.Interfaces;

internal interface IScheduledServiceFactory
{
    List<ScheduledService> GetScheduledServices();
}