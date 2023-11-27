using aspnetcore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public abstract class BaseService<TEntityDto, TEntity, TCreateDto, TUpdateDto> : BaseReadOnlyService<TEntityDto, TEntity>, IBaseService<TEntityDto, TCreateDto, TUpdateDto> where TEntity : IEntity
    {
        #region Constructor
        protected BaseService(IBaseRepository<TEntity> baseRepository) : base(baseRepository)
        {
        }
        #endregion

        #region Methods
        public async Task<TEntityDto> InsertAsync(TCreateDto createDto)
        {
            var entity = MapCreateDtoToEntity(createDto);

            if (entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());
            }

            if (entity is BaseAudit baseAudit)
            {
                baseAudit.CreatedBy ??= "VTThanh";
                baseAudit.CreatedDate ??= DateTime.Now;
                baseAudit.ModifiedDate ??= DateTime.Now;
                baseAudit.ModifiedBy ??= "VTThanh";
            }

            await ValidateCreateBussiness(entity);

            await BaseRepository.InsertAsync(entity);

            var result = MapEntityToDto(entity);

            return result;
        }

        public async Task<TEntityDto> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var entity = await BaseRepository.GetAsync(id);

            var newEntity = MapUpdateDtoToEntity(updateDto, entity);

            if (entity is BaseAudit baseAudit)
            {
                baseAudit.ModifiedDate ??= DateTime.Now;
                baseAudit.ModifiedBy ??= "VTThanh";
            }

            await ValidateUpdateBussiness(newEntity);

            await BaseRepository.UpdateAsync(newEntity);

            var result = MapEntityToDto(entity: newEntity);

            return result;
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);

            var result = await BaseRepository.DeleteAsync(entity);
            
            return result;
        }

        public async Task<int> DeleteManyAsync(List<Guid> ids)
        {
            var (entities, notExistIds) = await BaseRepository.GetListIdsAsync(ids);

            var result = await BaseRepository.DeleteManyAsync(entities);


            if (notExistIds.Count > 0 && notExistIds is not null)
            {
                var message = $"Không tìm thấy {string.Join(", ", notExistIds)}";
                throw new NotFoundException(message, (int)StatusCode.NotFound);
            }

            return result;
        }

        public abstract TEntity MapCreateDtoToEntity(TCreateDto createDto);
        public abstract TEntity MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity entity);

        public virtual async Task ValidateCreateBussiness(TEntity entity)
        {
            await Task.CompletedTask;
        }

        public virtual async Task ValidateUpdateBussiness(TEntity entity)
        {
            await Task.CompletedTask;
        }
        #endregion
    }
}
