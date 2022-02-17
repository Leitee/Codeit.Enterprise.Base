/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.DomainModel
{
    using System;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPagedListEntity<TEntity> where TEntity : IEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        IQueryable<TEntity> PagedEntities { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int CurrentPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int RowsPerPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int CollectionLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        double TotalPages { get; set; }

    }
}
