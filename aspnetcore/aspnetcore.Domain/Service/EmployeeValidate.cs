using aspnetcore.Domain.Resource;
using aspnetcore.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aspnetcore.Domain
{
    public class EmployeeValidate : IEmployeeValidate
    {
        // validate nghiệp vụ trong này: ngày tháng, mã, ...
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;


        public EmployeeValidate(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        //public async Task CheckDepartmentAsync(Department department)
        //{
        //    var isExistDepartment = await _departmentRepository.IsExitstDepartmentAsync(department.DepartmentId);

        //    if (!isExistDepartment)
        //    {
        //        throw new ConflictException(ResourceVN.Error_DepartmentNotFound, 409);
        //    }
        //}

        public async Task CheckEmployeeCodeAsync(Employee employee)
        {
            var isExistEmployee =  await _employeeRepository.IsExitstEmployeeCodeAsync(employee.EmployeeCode);   

            if(isExistEmployee)
            {
                throw new ConflictException($"Mã nhân viên <{employee.EmployeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.", (int)StatusCode.Conflict);
            }

        }

        public void DateValidateAsync(Employee employee)
        {
            DateTime currentDate = DateTime.Now.Date;

            if (employee.DateOfBirth != default)
            {
                if (employee.DateOfBirth > currentDate)
                {
                    throw new ConflictException(ResourceVN.Error_DateOfBirth, (int)StatusCode.Conflict);
                }
            }

            if (employee.IdentityDate != default)
            {
                if (employee.IdentityDate > currentDate)
                {
                    throw new ConflictException(ResourceVN.Error_IdentityDate, (int)StatusCode.Conflict);
                }
            }
        }

        public void EmailValidateAsync(Employee employee)
        {
            if(employee.Email != "" && employee.Email != null)
            {
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                bool isEmailValid = Regex.IsMatch(employee.Email, emailPattern);

                if (!isEmailValid)
                {
                    throw new ConflictException(ResourceVN.Error_InvalidEmail, (int)StatusCode.Conflict);
                }
            }
        }
    }
}
