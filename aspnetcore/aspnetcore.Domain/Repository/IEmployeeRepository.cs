using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    /// <summary>
    /// Interface tương tác với repository của Employee
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Phương thức lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<string> GetNewEmployeeCodeAsync();


        /// <summary>
        /// Phương thức kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>
        /// - true: mã bị trùng
        /// - false: mã không bị trùng
        /// </returns>
        /// Created by: VTThanh (10/9/2023)
        Task<bool> IsExitstEmployeeCodeAsync(string employeeCode);

        /// <summary>
        /// Phương thức lọc các bản ghi
        /// </summary>
        /// <param name="pageSize">Tổng số trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="searchText">Text tìm kiếm</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<List<Employee>> FilterAsync(int pageSize, int pageNumber, string searchText);

        /// <summary>
        /// Phương thức lọc các bản ghi
        /// </summary>
        /// <param name="pageSize">Tổng số trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="searchText">Text tìm kiếm</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<FilterResult<Employee>> FilterResultAsync(int pageSize, int pageNumber, string searchText);
    }
}
