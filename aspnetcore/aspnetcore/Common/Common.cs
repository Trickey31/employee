using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace aspnetcore
{
    public static class Common
    {
        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name = "employeeCode" > Mã nhân viên</param>
        /// <returns>
        ///     -> true: mã nhân viên bị trùng
        ///     -> false: mã nhân viên không bị trùng
        /// </returns>
        /// Created by: VTThanh (13/9/2023)
        public static bool IsDuplicateEmployeeCode(string employeeCode)
        {
            // Khai báo ConnectionString
            var connectionString = "Server=127.0.0.1;Port=3306;Database=MISA.Web07_MF1745_VTThanh;Uid=root;Pwd=123456;";

            // Tạo kết nối với CSDL
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sqlQuery = $"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";

            var param = new DynamicParameters();
            param.Add("EmployeeCode", employeeCode);


            var response = connection.QuerySingleOrDefault<string>(sqlQuery, param);

            Console.WriteLine(response);

            if (response != null)
            {
                return true;
            } 
            return false;
        }
    }
}
