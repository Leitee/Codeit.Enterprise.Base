/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.NetStdLibrary.Base.BusinessLogic
{
    using AutoMapper;
    using Codeit.NetStdLibrary.Base.Abstractions.BusinessLogic;
    using Codeit.NetStdLibrary.Base.Abstractions.DomainModel;
    using System;
    using System.Collections.Generic;

    public abstract class GenericMapper<TEntity, TDto> : IMapperCore<TEntity, TDto> where TEntity : IEntity<Guid> where TDto : IDto
    {
        /// <summary>
        /// 
        /// </summary>
        protected IMapper MappingConfiguration { get; set; }

        protected GenericMapper()
        {
            MappingConfiguration = CreateMapConfiguration();
        }

        protected IMapper DefaultMapConfiguration()
        {
            return new MapperConfiguration(c =>
            {
                //c.ForAllMaps((typeMap, mappingExpression) => mappingExpression.MaxDepth(2));
                //c.CreateMap<TEntity, TDto>().MaxDepth(2);
                c.CreateMap<TEntity, TDto>().MaxDepth(2).ReverseMap();
            }).CreateMapper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract IMapper CreateMapConfiguration();

        public virtual void SetMapperConfiguration(IMapperConfigurationExpression pConfigurationExpression)
        {
            //TODO: add expression param functionaity
            var expression = pConfigurationExpression;
        }

        public virtual void SetMapperConfiguration(IMapper pMapperConfig)
        {
            MappingConfiguration = pMapperConfig;
        }

        public virtual TDto Map(TEntity pEntity)
        {
            if (pEntity == null) return default;
            return MappingConfiguration.Map<TDto>(pEntity);
        }

        public virtual IEnumerable<TDto> Map(IEnumerable<TEntity> pEntities)
        {
            if (pEntities == null) return null;
            return MappingConfiguration.Map<IEnumerable<TDto>>(pEntities);
        }

        public virtual TEntity Map(TDto pDto)
        {
            if (pDto == null) return default;
            return MappingConfiguration.Map<TEntity>(pDto);
        }

        public virtual IEnumerable<TEntity> Map(IEnumerable<TDto> pDtos)
        {
            if (pDtos == null) return null;
            return MappingConfiguration.Map<IEnumerable<TEntity>>(pDtos);
        }
    }

    public class GenericMapper : IMapperCore
    {
        protected virtual IMapper CreateCustomMap<TInputEntity, TOutputEntity>()
        {
            return new MapperConfiguration(c =>
            {
                c.ForAllMaps((typeMap, mappingExpression) => mappingExpression.MaxDepth(1));
                c.CreateMap<TInputEntity, TOutputEntity>();
            }).CreateMapper();
        }

        public virtual TOutputEntity MapEntity<TInputEntity, TOutputEntity>(TInputEntity pEntity, IMapper pMapperConfig = null)
        {
            if (pEntity == null) return default;
            var mapper = pMapperConfig ?? CreateCustomMap<TInputEntity, TOutputEntity>();
            return mapper.Map<TOutputEntity>(pEntity);
        }

        public virtual IEnumerable<TOutputEntity> MapEntity<TInputEntity, TOutputEntity>(IEnumerable<TInputEntity> pEntities, IMapper pMapperConfig = null)
        {
            if (pEntities == null) return null;
            var mapper = pMapperConfig ?? CreateCustomMap<TInputEntity, TOutputEntity>();
            return mapper.Map<IEnumerable<TOutputEntity>>(pEntities);
        }

        public virtual TBaseClass MapToBaseClass<TDerivedClass, TBaseClass>(TDerivedClass pEntity)
        {
            if (pEntity == null) return default;
            return new MapperConfiguration(c =>
            {
                c.CreateMap<TDerivedClass, TBaseClass>();
            })
            .CreateMapper()
            .Map<TBaseClass>(pEntity);
        }

        public virtual IEnumerable<TBaseClass> MapToBaseClass<TDerivedClass, TBaseClass>(IEnumerable<TDerivedClass> pEntities)
        {
            if (pEntities == null) return null;
            return new MapperConfiguration(c =>
            {
                c.CreateMap<TDerivedClass, TBaseClass>();
            })
            .CreateMapper()
            .Map<IEnumerable<TBaseClass>>(pEntities);
        }
    }
}
