/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.DomainModel
{
    /// <summary>
    /// Enables logical deleting implementation.
    /// </summary>
    public interface IDeleteableEntity
    {
        bool Deleted { get; set; }
    }
}
