using aspnetcore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public abstract class BaseReadOnlyService<TEntityDto, TEntity> : IBaseReadOnlyService<TEntityDto>
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> BaseRepository;

        #endregion

        #region Constructor
        protected BaseReadOnlyService(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        #endregion

        #region Methods
        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAllAsync();

            var result = entities.Select(entity => MapEntityToDto(entity)).ToList();

            return result;
        }

        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);

            var result = MapEntityToDto(entity);

            return result;
        }

        public abstract TEntityDto MapEntityToDto(TEntity entity);

        #endregion
    }
}
