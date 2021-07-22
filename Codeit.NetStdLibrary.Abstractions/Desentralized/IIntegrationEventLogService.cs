using Microsoft.EntityFrameworkCore.Storage;
using Codeit.NetStdLibrary.Abstractions.Desentralized;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codeit.NetStdLibrary.Base.Abstractions.Desentralized
{
    public interface IIntegrationEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
        Task SaveEventAsync(IntegrationEventPayload @event, IDbContextTransaction transaction);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
