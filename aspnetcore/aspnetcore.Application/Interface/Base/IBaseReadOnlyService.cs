using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public interface IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Phương thức lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<List<TEntityDto>> GetAllAsync();

        
        /// <summary>
        /// Phương thức lấy bản ghi theo id
        /// </summary>
        /// <param name="id">định danh</param>
        /// <returns>Bản ghi</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<TEntityDto> GetAsync(Guid id);
    }
}
