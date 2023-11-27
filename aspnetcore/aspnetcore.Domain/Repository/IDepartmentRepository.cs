using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Phương thức kiểm tra phòng ban có tồn tại không
        /// </summary>
        /// <param name="departmentId">Mã phòng ban</param>
        /// <returns>
        /// - true: có phòng ban
        /// - false: không có phòng ban
        /// </returns>
        Task<bool> IsExitstDepartmentAsync(Guid departmentId);
    }
}
