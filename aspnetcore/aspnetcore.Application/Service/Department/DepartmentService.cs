using aspnetcore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Application
{
    public class DepartmentService : BaseReadOnlyService<DepartmentDto, Department>, IDepartmentService
    {
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
        }

        /// <summary>
        /// Phương thức map dữ liệu từ DepartmentEntity -> DepartmentDto
        /// </summary>
        /// <param name="department">department</param>
        /// <returns>EmployeeDto</returns>
        public override DepartmentDto MapEntityToDto(Department department)
        {
            var departmentDto = new DepartmentDto()
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };

            return departmentDto;
        }
    }
}
