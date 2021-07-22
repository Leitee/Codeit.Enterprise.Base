/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.DomainModel
{
    using Codeit.NetStdLibrary.Abstractions.DomainModel;
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagedListEntity<TEntity> : IPagedListEntity<TEntity> where TEntity : IEntity
    {
        public IQueryable<TEntity> PagedEntities { get; set; }
        public int CurrentPage { get; set; }
        public int RowsPerPage { get; set; }
        public int CollectionLength { get; set; }
        public double TotalPages { get; set; }
    }
}
