/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.DataAccess
{
    using Codeit.Enterprise.Base.Abstractions.DomainModel;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICrudRepository<TEntity, TId> where TEntity : class, IEntity<TId> where TId : struct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> AllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> AllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy, CancellationToken cancellationToken = default, params Expression<Func<IIncludable<TEntity>, IIncludable>>[] includes);

        /// <summary>
        /// Get an entity by ID which is tracked by EF to Delete or Update easily.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TId id);

        /// <summary>
        /// Get an entity by ID which is not tracked by EF in order to increase performcance. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(TId id);

        /// <summary>
        /// Get an entity by an expression which is not tracked by EF in order to increase performcance. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<IIncludable<TEntity>, IIncludable>>[] includes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entityToUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(TId id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entityToDelete);
    }
}
