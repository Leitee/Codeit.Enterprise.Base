/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.DataAccess
{
    using Codeit.NetStdLibrary.Abstractions.DomainModel;
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId">Id Type</typeparam>
    public interface IPaginableRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IPagedListEntity<TEntity>> AllPagedAsync(int skip, int take, int pageSize, int currentPage,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            CancellationToken cancellationToken = default,
            params Expression<Func<IIncludable<TEntity>, IIncludable>>[] includes);
    }
}
