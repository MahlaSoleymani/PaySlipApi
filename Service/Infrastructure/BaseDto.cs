﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Entities.Infrastructure;
using Service.Infrastructure.CustomMapping;
using Service.UserServices.Dto;

namespace Service.Infrastructure
{
    public abstract class BaseDto<TDto, TEntity, TKey> : IHaveCustomMapping
        where TDto : class, new()
        where TEntity : new()
    {
        [Display(Name = "ردیف")]
        public TKey Id { get; set; }
        public bool IsActive { get; set; } = true;

        public TEntity ToEntity(IMapper mapper)
        {
            return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
        }
        public IEnumerable<TEntity> ToListEntity(IMapper mapper)
        {
            return mapper.Map<IEnumerable<TEntity>>(CastToDerivedClass(mapper, this));
        }
        public TEntity ToEntity(IMapper mapper, TEntity entity)
        {
            return mapper.Map(CastToDerivedClass(mapper, this), entity);
        }

        public static IList<TDto> FromEntities(IMapper mapper, IList<TEntity> model)
        {
            return mapper.Map<IList<TDto>>(model);
        }
        public static TDto FromEntity(IMapper mapper, TEntity model)
        {
            return mapper.Map<TDto>(model);
        }
        public static TDto FromListEntity(IMapper mapper, IEnumerable<TEntity> model)
        {
            return mapper.Map<TDto>(model);
        }

        protected TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity, TKey> baseInstance)
        {
            return mapper.Map<TDto>(baseInstance);
        }

        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMappings(mappingExpression.ReverseMap());
        }

        public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
        {

        }



    }
    public abstract class BaseEmptyDto<TDto, TEntity, TKey> : IHaveCustomMapping
     where TDto : class, new()
     where TEntity : IEntity, new()
    {
        [Display(Name = "ردیف")]
        public TKey Id { get; set; }


        public TEntity ToEntity(IMapper mapper)
        {
            return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
        }
        public IEnumerable<TEntity> ToListEntity(IMapper mapper)
        {
            return mapper.Map<IEnumerable<TEntity>>(CastToDerivedClass(mapper, this));
        }
        public TEntity ToEntity(IMapper mapper, TEntity entity)
        {
            return mapper.Map(CastToDerivedClass(mapper, this), entity);
        }

        public static IList<TDto> FromEntities(IMapper mapper, IList<TEntity> model)
        {
            return mapper.Map<IList<TDto>>(model);
        }
        public static TDto FromEntity(IMapper mapper, TEntity model)
        {
            return mapper.Map<TDto>(model);
        }
        public static TDto FromListEntity(IMapper mapper, IEnumerable<TEntity> model)
        {
            return mapper.Map<TDto>(model);
        }

        protected TDto CastToDerivedClass(IMapper mapper, BaseEmptyDto<TDto, TEntity, TKey> baseInstance)
        {
            return mapper.Map<TDto>(baseInstance);
        }

        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMappings(mappingExpression.ReverseMap());
        }

        public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
        {

        }

    }

    public abstract class BaseDto<TDto, TEntity> : BaseDto<TDto, TEntity, long>
        where TDto : class, new()
        where TEntity : new()
    {
    }

    public abstract class BaseEmptyDto<TDto, TEntity> : BaseEmptyDto<TDto, TEntity, long>
        where TDto : class, new()
        where TEntity : IEntity, new()
    {

    }


    public abstract class BaseAuditableDto<TDto, TEntity, TKey, TUserDto, TUserKey> : BaseDto<TDto, TEntity, TKey>
        where TDto : class, new()
        where TEntity : new()
    {
        public string CreatedDateTime { get; set; }
        public TUserDto CreatedUser { get; set; }
        public string CreatedUserFullName { get; set; }
        public TUserKey CreatedUserId { get; set; }
        public string ModifiedDateTime { get; set; }
        public TUserDto ModifiedUser { get; set; }
        public string ModifiedUserFullName { get; set; }
        public TUserKey ModifiedUserId { get; set; }
    }
    public abstract class BaseAuditableDto<TDto, TEntity, TKey> : BaseAuditableDto<TDto, TEntity, TKey, UserDto, int>
        where TDto : class, new()
        where TEntity : IEntity, new()
    {

    }
    public abstract class BaseAuditableDto<TDto, TEntity> : BaseAuditableDto<TDto, TEntity, int, UserDto, int>
        where TDto : class, new()
        where TEntity : IEntity, new()
    {

    }
}
