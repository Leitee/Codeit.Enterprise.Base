/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.DomainModel
{
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPagedListEntity<TEntity> where TEntity : IEntity
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
