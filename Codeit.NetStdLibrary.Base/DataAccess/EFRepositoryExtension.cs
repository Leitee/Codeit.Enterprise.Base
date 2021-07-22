/// <summary>
/// 
/// </summary>
namespace System.Linq.Expressions
{
    using Codeit.NetStdLibrary.Base.Abstractions.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Codeit.NetStdLibrary.Base.DataAccess;

    /// <summary>
    /// 
    /// </summary>
    public static class EFRepositoryExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> query,
            params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> query,
            params Expression<Func<IIncludable<TEntity>, IIncludable>>[] includes) where TEntity : class
        {
            if (includes == null)
                return query;

            foreach (var include in includes)
            {
                var includable = (Includable<TEntity>)include.Compile()(new Includable<TEntity>(query));
                query = includable.Input;
            }

            return query;
        }
    }
}
