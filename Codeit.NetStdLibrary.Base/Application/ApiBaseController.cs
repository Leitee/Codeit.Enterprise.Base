using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Application
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public abstract class ApiBaseController : ControllerBase
    {
        protected readonly ILogger<ApiBaseController> _logger;

        protected ApiBaseController(ILoggerFactory loggerFactory)
        {
            _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<ApiBaseController>();
            _logger?.LogInformation($"Initializing controller {typeof(ApiBaseController)}");
        }
    }
}