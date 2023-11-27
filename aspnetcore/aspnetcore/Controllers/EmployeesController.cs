using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using System.Data;
using aspnetcore.Application;
using aspnetcore.Domain;
using ClosedXML.Excel;

namespace aspnetcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService) : base(employeeService) 
        { 
            _employeeService = employeeService;
        }


        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: VTThanh (13/9/2023)
        [HttpGet]
        [Route("NewEmployeeCode")]
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var response = await _employeeService.GetNewEmployeeCodeAsync();

            return response;
        }

        //[HttpGet("Filter")]
        //public async Task<List<EmployeeDto>> FilterAsync(int pageSize, int pageNumber, string? searchText)
        //{
        //    var response = await _employeeService.FilterAsync(pageSize, pageNumber, searchText);

        //    return response;
        //}

        [HttpGet("Filter")]
        public async Task<FilterResult<EmployeeDto>> FilterResultAsync(int pageSize, int pageNumber, string? searchText)
        {
            var response = await _employeeService.FilterResultAsync(pageSize, pageNumber, searchText);

            var filterResult = new FilterResult<EmployeeDto>
            {
                Results = response.Results,
                TotalCount = response.TotalCount
            };

            return filterResult;
        }

        [HttpGet("ExportExcel")]
        //public async Task<IActionResult> ExportExcel()
        //{
        //    var excelStream = await _employeeService.ExportExcelAsync();

        //    return File(excelStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien.xlsx");
        //}
        public async Task<IActionResult> ExportExcel()
        {
            var employeeData = await _employeeService.ExportExcelAsync();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(employeeData, "Danh sách nhân viên");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
                }
            }

        }
    }
}
