using AutoMapper;
using aspnetcore.Domain;
using aspnetcore.Domain.Service;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace aspnetcore.Application
{
    public class EmployeeService : BaseService<EmployeeDto, Employee, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        #region Fields
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidate _employeeValidate;
        private readonly IMapper _mapper;

        #endregion


        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeValidate employeeValidate, IMapper mapper) : base(employeeRepository) 
        {
            _employeeRepository = employeeRepository;
            _employeeValidate = employeeValidate;
            _mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var employeeCode = await _employeeRepository.GetNewEmployeeCodeAsync();

            return employeeCode;
        }

        public async Task<List<EmployeeDto>> FilterAsync(int pageSize, int pageNumber, string searchText)
        {
            var response = await _employeeRepository.FilterAsync(pageSize, pageNumber, searchText);

            var result = response.Select(entity => MapEntityToDto(entity)).ToList();

            return result;
        }


        public override async Task ValidateCreateBussiness(Employee employee)
        {
            await _employeeValidate.CheckEmployeeCodeAsync(employee);

            await _employeeValidate.DateValidateAsync(employee);

            await _employeeValidate.EmailValidateAsync(employee);
        }

        public override async Task ValidateUpdateBussiness(Employee employee)
        {
            var oldEmployee = await _employeeRepository.GetAsync(employee.EmployeeId);

            if(oldEmployee.EmployeeCode != employee.EmployeeCode)
            {
                await _employeeValidate.CheckEmployeeCodeAsync(employee);
            }

            if(oldEmployee.DateOfBirth != employee.DateOfBirth) 
            { 
                await _employeeValidate.DateValidateAsync(employee);
            }

            if(oldEmployee.Email != employee.Email)
            {
                await _employeeValidate.EmailValidateAsync(employee);
            }
        }

        /// <summary>
        /// Phương thức map dữ liệu từ EmployeeDto -> EmployeeEntity
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>EmployeeDto</returns>
        public override Employee MapCreateDtoToEntity(EmployeeCreateDto employeeCreateDto)
        {
            var employeeEntity = _mapper.Map<Employee>(employeeCreateDto);

            return employeeEntity;
        }

        /// <summary>
        /// Phương thức map dữ liệu từ EmployeeDto -> EmployeeEntity
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>EmployeeDto</returns>
        public override Employee MapUpdateDtoToEntity(EmployeeUpdateDto employeeUpdateDto, Employee employee)
        {
            var employeeEntity = _mapper.Map(employeeUpdateDto, employee);

            return employeeEntity;
        }

        /// <summary>
        /// Phương thức map dữ liệu từ EmployeeEntity -> EmployeeDto
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>EmployeeDto</returns>
        public override EmployeeDto MapEntityToDto(Employee employee)
        {
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public async Task<FilterResult<EmployeeDto>> FilterResultAsync(int pageSize, int pageNumber, string searchText)
        {
            var response = await _employeeRepository.FilterResultAsync(pageSize, pageNumber, searchText);
            var result = response.Results.Select(entity => MapEntityToDto(entity)).ToList();

            var filterResult = new FilterResult<EmployeeDto>
            {
                Results = result,
                TotalCount = response.TotalCount
            };

            return filterResult;
        }

        public async Task<System.Data.DataTable> ExportExcelAsync()
        {
            var employeeData = await GetEmpData();

            return employeeData;
        }
        public async Task<System.Data.DataTable> GetEmpData()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.TableName = "Danh sách nhân viên";
            dt.Columns.Add("Mã nhân viên", typeof(string));
            dt.Columns.Add("Họ và tên", typeof(string));
            dt.Columns.Add("Giới tính", typeof(string));
            dt.Columns.Add("Ngày sinh", typeof(string));
            dt.Columns.Add("Phòng ban", typeof(string));
            dt.Columns.Add("Chức danh", typeof(string));
            dt.Columns.Add("Tài khoản ngân hàng", typeof(string));
            dt.Columns.Add("Tên ngân hàng", typeof(string));

            var employees = await _employeeRepository.GetAllAsync();
            if (employees.Count > 0)
            {
                employees.ForEach(employee => dt.Rows.Add(employee.EmployeeCode, employee.FullName, employee.Gender, employee.DateOfBirth, employee.DepartmentName, employee.PositionName, employee.BankAccount, employee.BankName));
            }

            return dt;
        }

        //public async Task<MemoryStream> ExportExcelAsync()
        //{
        //    var employeeData = await GetEmptyData();
        //    var excelPackage = new ExcelPackage();

        //    var worksheet = excelPackage.Workbook.Worksheets.Add("Danh sách nhân viên");
        //    worksheet.Cells["A1:P1"].Style.Font.Bold = true;
        //    worksheet.Cells["A1:P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //    worksheet.Cells["A1:P1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

        //    // Thiết lập độ rộng cho các cột
        //    worksheet.Column(1).Width = 15;
        //    worksheet.Column(2).Width = 30;
        //    worksheet.Column(3).Width = 15;
        //    worksheet.Column(4).Width = 15;
        //    worksheet.Column(5).Width = 30;
        //    worksheet.Column(6).Width = 20;
        //    worksheet.Column(7).Width = 20;
        //    worksheet.Column(8).Width = 40;

        //    worksheet.Cells["A1:P1"].LoadFromDataTable(employeeData, true);

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        excelPackage.SaveAs(memoryStream);
        //        return memoryStream;
        //    }
        //}

        //public async Task<System.Data.DataTable> GetEmptyData()
        //{
        //    var dt = new System.Data.DataTable();
        //    dt.TableName = "DANH SÁCH NHÂN VIÊN";
        //    dt.Columns.Add("Mã nhân viên", typeof(string));
        //    dt.Columns.Add("Họ và tên", typeof(string));
        //    dt.Columns.Add("Giới tính", typeof(string));
        //    dt.Columns.Add("Ngày sinh", typeof(string));
        //    dt.Columns.Add("Tên đơn vị", typeof(string));
        //    dt.Columns.Add("Chức danh", typeof(string));
        //    dt.Columns.Add("Tài khoản ngân hàng", typeof(string));
        //    dt.Columns.Add("Tên ngân hàng", typeof(string));

        //    var employees = await _employeeRepository.GetAllAsync();
        //    foreach (var employee in employees)
        //    {
        //        dt.Rows.Add(
        //            employee.EmployeeCode,
        //            employee.FullName,
        //            employee.Gender,
        //            employee.DateOfBirth,
        //            employee.DepartmentName,
        //            employee.PositionName,
        //            employee.BankAccount,
        //            employee.BankName
        //        );
        //    }

        //    return dt;
        //}

        #endregion
    }
}
