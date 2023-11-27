using aspnetcore.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        /// <summary>
        /// Phương thức lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<string> GetNewEmployeeCodeAsync();

        /// <summary>
        /// Phương thức lọc các bản ghi
        /// </summary>
        /// <param name="pageSize">Tổng số trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="searchText">Text tìm kiếm</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<List<EmployeeDto>> FilterAsync(int pageSize, int pageNumber, string searchText);

        /// <summary>
        /// Phương thức lọc các bản ghi
        /// </summary>
        /// <param name="pageSize">Tổng số trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="searchText">Text tìm kiếm</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: VTThanh (10/9/2023)
        Task<FilterResult<EmployeeDto>> FilterResultAsync(int pageSize, int pageNumber, string searchText);


        /// <summary>
        /// Export to excel
        /// </summary>
        /// <returns>File excel</returns>
        /// Created by: VTThanh (10/9/2023)
        //Task<MemoryStream> ExportExcelAsync();
        Task<DataTable> ExportExcelAsync();

    }
}
