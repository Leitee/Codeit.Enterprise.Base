/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A factory that creates Repositories.
    /// </summary>
    /// <remarks>
    /// An instance of this class contains repository factory functions for different types.
    /// Each factory function takes an EF <see cref="DbContext"/> and returns
    /// a repository bound to that DbContext.
    /// <para>
    /// Designed to be a "Singleton", configured at web application start with
    /// all of the factory functions needed to create any type of repository.
    /// Should be thread-safe to use because it is configured at app start,
    /// before any request for a factory, and should be immutable thereafter.
    /// </para>
    /// </remarks>
    public abstract class RepositoryFactories<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Get the dictionary of repository factory functions.
        /// </summary>
        /// <remarks>
        /// A dictionary key is a System.Type, typically a repository type.
        /// A value is a repository factory function
        /// that takes a <see cref="DbContext"/> argument and returns
        /// a repository object. Caller must know how to cast it.
        /// </remarks>
        private readonly IDictionary<Type, Func<TContext, object>> _repositoryFactories;

        /// <summary>
        /// Constructor that initializes with runtime repository factories
        /// </summary>
        protected RepositoryFactories()
        {
            _repositoryFactories = GetCustomeFactories();
        }

        /// <summary>
        /// Return the runtime Code Camper repository factory functions,
        /// each one is a factory for a repository of a particular type.
        /// </summary>
        /// <remarks>
        /// MODIFY THIS METHOD TO ADD CUSTOM FACTORY FUNCTIONS
        /// </remarks>
        protected virtual IDictionary<Type, Func<TContext, object>> GetCustomeFactories()
        {
            return new Dictionary<Type, Func<TContext, object>>
            {
                //{typeof(IUserRepository), _dbContext => userRepository},
                //{typeof(IRoleRepository), _dbContext => roleRepository},
            };
        }

        /// <summary>
        /// Get the repository factory function for the type.
        /// </summary>
        /// <typeparam name="T">Type serving as the repository factory lookup key.</typeparam>
        /// <returns>The repository function if found, else null.</returns>
        /// <remarks>
        /// The type parameter, T, is typically the repository type 
        /// but could be any type (e.g., an entity type)
        /// </remarks>
        public Func<TContext, object> GetRepositoryFactory<T>()
        {
            _repositoryFactories.TryGetValue(typeof(T), out Func<TContext, object> factory);
            return factory;
        }

        /// <summary>
        /// Get the factory for <see cref="IRepository{T}"/> where T is an entity type.
        /// </summary>
        /// <typeparam name="TEntity">The root type of the repository, typically an entity type.</typeparam>
        /// <returns>
        /// A factory that creates the <see cref="IRepository{T}"/>, given an EF <see cref="DbContext"/>.
        /// </returns>
        /// <remarks>
        /// Looks first for a custom factory in <see cref="_repositoryFactories"/>.
        /// If not, falls back to the <see cref="DefaultEntityRepositoryFactory{T}"/>.
        /// You can substitute an alternative factory for the default one by adding
        /// a repository factory for type "T" to <see cref="_repositoryFactories"/>.
        /// </remarks>
        public Func<TContext, object> GetRepositoryFactoryForEntityType<TEntity>() where TEntity : class, IEntity
        {
            return GetRepositoryFactory<TEntity>() ?? DefaultEntityRepositoryFactory<TEntity>();
        }

        /// <summary>
        /// Default factory for a <see cref="IRepository{T}"/> where T is an entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the repository's root entity</typeparam>
        protected abstract Func<TContext, object> DefaultEntityRepositoryFactory<TEntity>() where TEntity : class, IEntity;
    }
}
