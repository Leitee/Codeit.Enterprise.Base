/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.DataAccess
{
    using Microsoft.EntityFrameworkCore.Storage;
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationUow : IDisposable
    {
        /// <summary>
        /// Get a repository entity by type from db context.
        /// </summary>
        /// <typeparam name="TEntity">Type of contect entity.</typeparam>
        /// <returns>Repository</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity<Guid>;

        /// <summary>
        /// Save pending changes to the database and return true if there was at least 1 row affected
        /// </summary>
        bool Commit();

        /// <summary>
        /// Save pending changes to the database asyncly and return true if there was at least 1 row affected
        /// </summary>
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Save pending changes to the database asyncly and return true if there was at least 1 row affected
        /// </summary>
        /// <summary>
        /// For transaction handling
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction StartTransaction();

        /// <summary>
        /// For transaction handling asyncly
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> StartTransactionAsync(CancellationToken cancellationToken = default);
    }
}
