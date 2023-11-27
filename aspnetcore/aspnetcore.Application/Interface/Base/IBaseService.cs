using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public interface IBaseService<TEntityDto, TCreateDto, TUpdateDto> : IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Phương thức thêm mới bản ghi
        /// </summary>
        /// <param name="createDto">Bản ghi được thêm</param>
        /// <returns>Bản ghi được thêm</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<TEntityDto> InsertAsync(TCreateDto createDto);

        /// <summary>
        /// Phương thức cập nhật bản ghi
        /// </summary>
        /// <param name="id">định danh bản ghi</param>
        /// <param name="updateDto">bản ghi mới</param>
        /// <returns>bản ghi được cập nhật</returns>
        Task<TEntityDto> UpdateAsync(Guid id, TUpdateDto updateDto);

        /// <summary>
        /// Phương thức xóa bản ghi
        /// </summary>
        /// <param name="id">định danh</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Phương thức xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách định danh</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<int> DeleteManyAsync(List<Guid> ids);
    }
}
