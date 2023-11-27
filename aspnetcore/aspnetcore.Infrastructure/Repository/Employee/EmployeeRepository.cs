using aspnetcore.Domain;
using MySqlConnector;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aspnetcore.Application;
using static Dapper.SqlMapper;

namespace aspnetcore.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion


        #region Methods
        public async Task<List<Employee>> FilterAsync(int pageSize, int pageNumber, string searchText)
        {
            var sqlQuery = $"SELECT * FROM View_Employees WHERE EmployeeCode LIKE @searchText OR FullName LIKE @searchText LIMIT @pageSize OFFSET @offset";

            var param = new DynamicParameters();
            param.Add("searchText", $"%{searchText}%");
            param.Add("pageSize", pageSize);
            param.Add("offset", (pageNumber - 1) * pageSize);

            var response = await UnitOfWork.Connection.QueryAsync<Employee>(sqlQuery, param, transaction: UnitOfWork.Transaction);

            var results = response.ToList();

            return results;
        }

        public async Task<FilterResult<Employee>> FilterResultAsync(int pageSize, int pageNumber, string searchText)
        {
            var sqlQuery = $"SELECT COUNT(*) FROM View_Employees WHERE EmployeeCode LIKE @searchText OR FullName LIKE @searchText; " +
                   $"SELECT * FROM View_Employees WHERE EmployeeCode LIKE @searchText OR FullName LIKE @searchText LIMIT @pageSize OFFSET @offset";

            var param = new DynamicParameters();
            param.Add("searchText", $"%{searchText}%");
            param.Add("pageSize", pageSize);
            param.Add("offset", (pageNumber - 1) * pageSize);

            var response = await UnitOfWork.Connection.QueryMultipleAsync(sqlQuery, param, transaction: UnitOfWork.Transaction);

            var totalCount = await response.ReadSingleAsync<int>();
            var results = await response.ReadAsync<Employee>();

            return new FilterResult<Employee>
            {
                Results = results.ToList(),
                TotalCount = totalCount
            };
        }

        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var sqlQuery = $"SELECT MAX(EmployeeCode) FROM Employee";

            var newestCode = await UnitOfWork.Connection.QuerySingleOrDefaultAsync<string>(sqlQuery, transaction: UnitOfWork.Transaction);

            string result;

            if(!string.IsNullOrEmpty(newestCode))
            {
                string prefix = newestCode.Substring(0, newestCode.Length - 5);

                bool parsed = int.TryParse(newestCode.Substring(newestCode.Length - 5), out int increamentedValue);

                if(parsed)
                {
                    increamentedValue++;
                    result = $"{prefix}{increamentedValue:D5}";
                }
                else
                {
                    result = $"{newestCode}-00001";
                }
            }
            else
            {
                result = $"NV-00001";
            }
            return result ;
        }


        public async Task<bool> IsExitstEmployeeCodeAsync(string employeeCode)
        {
            // Tạo câu truy vấn
            var sqlQuery = $"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";

            var param = new DynamicParameters();
            param.Add("EmployeeCode", employeeCode);


            var response = await UnitOfWork.Connection.QuerySingleOrDefaultAsync<Employee>(sqlQuery, param, transaction: UnitOfWork.Transaction);

            Console.WriteLine(response);

            if (response != null)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
