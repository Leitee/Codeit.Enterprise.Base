/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.DataAccess
{
    using Codeit.Enterprise.Base.Abstractions.DomainModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : ICrudRepository<TEntity, Guid>, IPaginableRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetCountByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> ExecuteQueryAsync(string query, CancellationToken cancellationToken = default, params object[] paramaters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<TEntity>> ExecSp(string spName, CancellationToken cancellationToken = default, params object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
