using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Codeit.NetStdLibrary.Abstractions.BusinessLogic;
using Codeit.NetStdLibrary.Base.Abstractions.Desentralized;
using Codeit.NetStdLibrary.Base.Desentralized.IntegrationEvent;
using System.Net;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Codeit.NetStdLibrary.Base.Application
{
    public class SendCrudOperationResultFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;
        public SendCrudOperationResultFilter(ILoggerFactory loggerFactory, IEventBus eventBus)
        {
            _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<SingleLoggerAsyncFilter>();
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
                var payload = new CrudNotificationIntegrationEventPayload(connId, callbackName, GetOperationType(request), succedded);
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
