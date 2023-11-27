using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Domain.Service
{
    public interface IEmployeeValidate
    {
        /// <summary>
        /// Kiểm tra có tồn tại EmployeeCode chưa
        /// </summary>
        /// <param name="employee">nhân viên</param>
        /// <exception cref="ConflictException"></exception>
        /// Created by: VTThanh (10/9/2023)
        Task CheckEmployeeCodeAsync(Employee employee);

        /// <summary>
        /// Kiểm tra ngày hợp lệ
        /// </summary>
        /// <param name="employee">nhân viên</param>
        /// <exception cref="ConflictException"></exception>
        /// Created by: VTThanh (10/9/2023)
        void DateValidateAsync(Employee employee);

        /// <summary>
        /// Kiểm tra email hợp lệ
        /// </summary>
        /// <param name="employee">nhân viên</param>
        /// <exception cref="ConflictException"></exception>
        /// Created by: VTThanh (10/9/2023)
        void EmailValidateAsync(Employee employee);

        /// <summary>
        /// Kiểm tra phòng ban
        /// </summary>
        /// <param name="department">phòng ban</param>
        /// <exception cref="ConflictException"></exception>
        /// Created by: VTThanh (10/9/2023)
        //Task CheckDepartmentAsync(Department department);
    }
}
