using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Codeit.NetStdLibrary.Base.Application
{
    public class SingleLoggerAsyncFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;
        public SingleLoggerAsyncFilter(ILoggerFactory loggerFactory)
        {
            _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<SingleLoggerAsyncFilter>();
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            var controller = context.Controller;
            _logger.LogInformation("Entering in controller: {controller}", controller);
            var result = await next();
            // execute any code after the action executes
            var code = (result.Result as ObjectResult)?.StatusCode ?? (int)HttpStatusCode.NoContent;
            _logger.LogDebug("Exiting controller with response code {code}", code);
        }
    }
}
