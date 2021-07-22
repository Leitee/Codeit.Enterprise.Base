/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.DomainModel
{
    /// <summary>
    /// Enables logical deleting implementation.
    /// </summary>
    public interface IDeleteableEntity
    {
        bool Deleted { get; set; }
    }
}
