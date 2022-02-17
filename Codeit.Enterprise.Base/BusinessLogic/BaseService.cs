/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.BusinessLogic
{
    using Codeit.Enterprise.Base.Abstractions.BusinessLogic;
    using Codeit.Enterprise.Base.Abstractions.DataAccess;
    using Codeit.Enterprise.Base.Abstractions.DomainModel;
    using Codeit.Enterprise.Base.Common;
    using Codeit.Enterprise.Base.DataAccess;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using System;

    public abstract class BaseService
    {
        protected readonly ILogger<BaseService> _logger;

        protected BaseService(ILoggerFactory loggerFactory)
        {            
            _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<BaseService>();
            _logger?.LogInformation($"Initializing service {typeof(BaseService)}");
        }        
    }

    public abstract class BaseService<TUow> : BaseService where TUow : IApplicationUow
    {
        protected readonly TUow _uow;
        protected readonly BLSettings _settings;
        protected readonly ExceptionFormatter _formatter;

        protected BaseService(TUow applicationUow, ILoggerFactory loggerFactory, IConfiguration configuration) : base(loggerFactory)
        {
            _formatter = new ExceptionFormatter(configuration ?? throw new BusinessLogicLayerException(nameof(configuration)));
            _settings = BLSettings.GetSettings(configuration);
            _uow = applicationUow;
        }
        /// <summary>
        /// Exceptions coming from DAL will pass througth otherwise will be return as a BL Exception 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>An exception to be thrown</returns>
        protected Exception HandleSVCException(Exception ex)
        {
            if (ex is DataAccessLayerException)
                return ex;

            return new BusinessLogicLayerException(ex);
        }

        protected void HandleSVCException(IBLResponse pResponse, Exception pEx)
        {
            _formatter.FormatException(ref pResponse, pEx);
        }

        protected void HandleSVCException(IBLResponse pResponse, params string[] pErrors)
        {
            _formatter.FormatException(ref pResponse, pErrors);
        }
    }

    public abstract class BaseService<TEntity, TDto> : BaseService<IApplicationUow> where TEntity : IEntity<Guid> where TDto : IDto
    {
        protected readonly IMapperCore<TEntity, TDto> _mapper;

        protected BaseService(IApplicationUow applicationUow, ILoggerFactory loggerFactory, IConfiguration configuration, IMapperCore<TEntity, TDto> mapperCore)
            : base(applicationUow, loggerFactory, configuration)
        {
            _mapper = mapperCore;
        }
    }
}
