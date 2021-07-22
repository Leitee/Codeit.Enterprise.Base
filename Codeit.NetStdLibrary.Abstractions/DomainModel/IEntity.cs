/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.DomainModel
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IEntity : IEntity<Guid> { }

    /// <summary>
    /// Provides a generic type Id to be used accross application.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Entity unique identifier accros application. 
        /// </summary>
        TId Id { get; set; }
    }
}
