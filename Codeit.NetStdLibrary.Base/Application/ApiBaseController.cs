/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Application
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// In order to implement this base controller a ApiVersioning package is needed. All endpoints are securized by default.
    /// </summary>
    /// <typeparam name="TController"></typeparam>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public abstract class ApiBaseController<TController> : ControllerBase
    {
        protected readonly ILogger<TController> _logger;

        protected ApiBaseController(ILoggerFactory loggerFactory)
        {
            _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<TController>();
            _logger?.LogInformation("Initializing controller {controller}", typeof(TController));
        }
    }

    public abstract class ApiBaseController : ApiBaseController<ApiBaseController>
    {
        protected ApiBaseController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {

        }
    }
}