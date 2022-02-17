/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Tests
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Moq;
    using Codeit.Enterprise.Base.Abstractions.DataAccess;
    using Codeit.Enterprise.Base.Abstractions.DomainModel;
    using System;
    using System.Threading;

    public abstract class TestServicesSutBase<TService, TEntity> : IDisposable where TEntity : class, IEntity<Guid>
    {
        private readonly Mock<IApplicationUow> _uowMock = new Mock<IApplicationUow>();
        protected Mock<IRepository<TEntity>> _repoEntityMock = new Mock<IRepository<TEntity>>();
        protected readonly TService _sut;

        public TestServicesSutBase()
        {
            _uowMock.Setup(x => x.Commit()).Returns(true);
            _uowMock.Setup(x => x.CommitAsync(CancellationToken.None)).ReturnsAsync(() => true);
            _uowMock.Setup(x => x.GetRepository<TEntity>()).Returns(_repoEntityMock.Object);
            _sut = CreateServiceInstance(_uowMock.Object, NullLoggerFactory.Instance, new ConfigurationBuilder().Build());
        }

        protected abstract TService CreateServiceInstance(IApplicationUow applicationUow, ILoggerFactory loggerFactory, IConfiguration configuration);

        public void Dispose()
        {
            _uowMock.Object.Dispose();
        }
    }
}
