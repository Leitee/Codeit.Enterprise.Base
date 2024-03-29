﻿/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.DomainModel
{
    /// <summary>
    /// Provides a generic type Id to be used across application.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntity<TId> : IEntity
    {
        /// <summary>
        /// Entity unique identifier across application. 
        /// </summary>
        TId Id { get; set; }
    }

    public interface IEntity
    {

    }

}
