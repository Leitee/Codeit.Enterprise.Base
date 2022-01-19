/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic
{
    using AutoMapper;
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System;
    using System.Collections.Generic;

    //public interface IMapperCore<in TInputEntity, out TOutputEntity>
    //{
    //    void SetMapperConfiguration(IMapper pMapperConfig);
    //    TOutputEntity MapEntity(TInputEntity pEntity);
    //    IEnumerable<TOutputEntity> MapEntity(IEnumerable<TInputEntity> pEntities);
    //}

    /// <summary>
    /// Generic mapper interface to be implemented. 
    /// </summary>
    /// <typeparam name="TEntity">A model enity</typeparam>
    /// <typeparam name="TDto">A dto class</typeparam>
    public interface IMapperCore<TEntity, TDto> where TEntity : IEntity where TDto : IDto
    {
        /// <summary>
        /// Set a new mapper and overrides the current map configuration.
        /// </summary>
        /// <param name="pMapperConfig">A mapper configuration</param>
        void SetMapperConfiguration(IMapper pMapperConfig);
        /// <summary>
        /// Gest a new DTO class of type <typeparamref name="TDto"/> from an entity of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="pEntity">A model entity to be mapped</param>
        /// <returns>A new Dto class</returns>
        TDto Map(TEntity pEntity);
        /// <summary>
        /// Gets a new entity of type <typeparamref name="TEntity"/> from a DTO class of type <typeparamref name="TDto"/>.
        /// </summary>
        /// <param name="pDto">A DTO class to be mapped</param>
        /// <returns>A new model Entity</returns>
        TEntity Map(TDto pDto);
        /// <summary>
        /// Gest a new DTO list of type <typeparamref name="TDto"/> from an entity list of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="pEntities">A list of entity</param>
        /// <returns>A new list of DTO</returns>
        ICollection<TDto> Map(ICollection<TEntity> pEntities);
        /// <summary>
        /// Gets a new entity list of type <typeparamref name="TEntity"/> from a DTO list of type <typeparamref name="TDto"/>.
        /// </summary>
        /// <param name="pDtos">A list of DTO</param>
        /// <returns>A new list of model entity</returns>
        ICollection<TEntity> Map(ICollection<TDto> pDtos);
    }
    public interface IMapperCore
    {
        TOutputEntity MapEntity<TInputEntity, TOutputEntity>(TInputEntity pEntity, IMapper pMapperConfig = null);
        ICollection<TOutputEntity> MapEntity<TInputEntity, TOutputEntity>(ICollection<TInputEntity> pEntities, IMapper pMapperConfig = null);
        TBaseClass MapToBaseClass<TDerivedClass, TBaseClass>(TDerivedClass pEntity);
        ICollection<TBaseClass> MapToBaseClass<TDerivedClass, TBaseClass>(ICollection<TDerivedClass> pEntities);
    }
}
