using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Codeit.Enterprise.Base.Abstractions.Tests;
using Codeit.Enterprise.Base.Application;
using System.Threading.Tasks;
using Xunit;

namespace Codeit.Enterprise.Base.Tests
{
    /// <summary>
    /// Base clase to be inherited for Controllers test scenarios without database dependency
    /// </summary>
    /// <typeparam name="TFixture"></typeparam>
    /// <typeparam name="TController"></typeparam>
    public abstract class TestScenarioControllerBase<TFixture, TController> : IClassFixture<TFixture> where TFixture : class, IHostFixture where TController : ApiBaseController
    {
        private readonly TFixture _testHost;
        protected ILoggerFactory _loggerMocked;
        protected TController Controller { get; private set; }
        protected IWebHost Host { get { return _testHost.Host; } }

        protected TestScenarioControllerBase(TFixture hostFixture)
        {
            _testHost = hostFixture;
            Controller = CreateControllerInstance();
        }
        /// <summary>
        /// Implement this method in order to return a new instance of the controller you want to put under test
        /// </summary>
        /// <returns></returns>
        protected abstract TController CreateControllerInstance();
        /// <summary>
        /// Get an intance form the DI container 
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <returns></returns>
        protected TInstance GetContainerInstance<TInstance>() where TInstance : class
        {
            return Host.Services.GetService(typeof(TInstance)) as TInstance;
        }
    }
    /// <summary>
    /// Base clase to be inherited for Controllers test scenarios using a DB context
    /// </summary>
    /// <typeparam name="TFixture"></typeparam>
    /// <typeparam name="TController"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class TestScenarioControllerBase<TFixture, TController, TContext>
        : TestScenarioControllerBase<TFixture, TController> where TContext : DbContext where TController : ApiBaseController where TFixture : class, IHostFixture
    {
        protected readonly TContext Context;

        protected TestScenarioControllerBase(TFixture hostFixture) : base(hostFixture)
        {
            TContext mockedCtx = Host.Services.GetService(typeof(TContext)) as TContext;
            SetSeedingStep(mockedCtx);
            mockedCtx.Database.EnsureCreated();
        }
        /// <summary>
        /// Implement this method in order to define your seeding logic according to your context
        /// </summary>
        /// <param name="context"></param>
        protected abstract void SetSeedingStep(TContext context);

    }

    public static class TestExtension
    {
        /// <summary>
        /// To Convert form IActionResul to a more appropiate BLResponse type
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        public static TResult ToBLResponse<TResult>(this Task<IActionResult> actionResult)
        {
            return (TResult)(actionResult.GetAwaiter().GetResult() as ObjectResult).Value;
        }
    }
}
