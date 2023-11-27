using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Phương thức lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<List<TEntity>> GetAllAsync();


        /// <summary>
        /// Phương thức lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Định danh bản ghi</param>
        /// <exception cref="NotFoundException">Không tìm thấy</exception>
        /// <returns>
        /// - Trả về thông tin bản ghi
        /// - Nếu không tìm thấy, trả về Exception
        /// </returns>
        /// Created by: VTThanh (10/9/2023)
        Task<TEntity> GetAsync(Guid id);


        /// <summary>
        /// Phương thức tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Định danh bản ghi</param>
        /// <exception cref="NotFoundException">Khi không tìm thấy</exception>
        /// <returns>
        ///     -> Trả về thông tin bản ghi
        ///     -> Nếu không tìm thấy, trả về null
        /// </returns>
        /// Created by: VTThanh (10/9/2023)
        Task<TEntity?> FindAsync(Guid id);


        /// <summary>
        /// Phương thức lấy danh sách id
        /// </summary>
        /// <param name="ids">Danh sách định danh bản ghi</param>
        /// <returns>
        /// List<TEntity>: Danh sách bản ghi
        /// List<Guid>: Danh sách id không tồn tại
        /// </returns>
        Task<(List<TEntity>, List<Guid>)> GetListIdsAsync(List<Guid> ids);


        /// <summary>
        /// Phương thức thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Nhân viên được thêm</param>
        /// <returns>Nhân viên được thêm</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<TEntity> InsertAsync(TEntity entity);



        /// <summary>
        /// Phương thức cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="entity"> Thông tin bản ghi được cập nhật</param>
        /// <returns>Nhân viên được cập nhật</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<TEntity> UpdateAsync(TEntity entity);



        /// <summary>
        /// Phương thức xóa bản ghi
        /// </summary>
        /// <param name="entity">Nhân viên cần xóa</param>
        /// <returns>Nhân viên bị xóa</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<int> DeleteAsync(TEntity entity);



        /// <summary>
        /// Phương thức xóa nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<int> DeleteManyAsync(List<TEntity> entities);
    }
}
