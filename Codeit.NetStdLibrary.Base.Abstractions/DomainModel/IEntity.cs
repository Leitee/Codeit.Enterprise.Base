/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.DomainModel
{
    /// <summary>
    /// Provides a generic type Id to be used across application.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Entity unique identifier across application. 
        /// </summary>
        TId Id { get; set; }
    }
}
