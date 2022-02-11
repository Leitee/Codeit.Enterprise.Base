/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.Application
{
    using Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic;
    using Codeit.NetStdLibrary.Base.Abstractions.Desentralized;
    using Codeit.NetStdLibrary.Base.Desentralized.IntegrationEvent;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using System.Net;
    using System.Threading.Tasks;
    using ILogger = Microsoft.Extensions.Logging.ILogger;

    public class SendCrudOperationResultFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;
        public SendCrudOperationResultFilter(ILoggerFactory loggerFactory, IEventBus eventBus)
        {
            _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<SendCrudOperationResultFilter>();
            _eventBus = eventBus;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var action = await next();

            var request = context.HttpContext.Request;
            if (request.Headers.TryGetValue("wsconnectionid", out var connId) && request.Headers.TryGetValue("wscallbackname", out var callbackName))
            {
                var statusCode = (action.Result as ObjectResult)?.StatusCode;
                var succedded = statusCode.HasValue && statusCode.Value == (int)HttpStatusCode.OK;
                var msg = succedded ? "PRODUCT CREATED" : "PRODUCT CREATION FAILED";
                _logger.LogDebug($"Publishing notification event: {msg} to client id:{connId}.");
                var payload = new ClientNotificationIntegrationEventPayload(connId, callbackName, GetOperationType(request), succedded);
                _eventBus.Publish(payload);
            }
        }

        private CrudOperationEnum GetOperationType(HttpRequest request)
        {
            return request.Method switch
            {
                "POST" => CrudOperationEnum.CREATE,
                "PUT" or "PATCH" => CrudOperationEnum.UPDATE,
                "DELETE" => CrudOperationEnum.DELETE,
                _ => CrudOperationEnum.READ,
            };
        }
    }
}
