using Dapper;
using aspnetcore.Application;
using aspnetcore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.Infrastructure
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> IsExitstDepartmentAsync(Guid departmentId)
        {
            // Tạo câu truy vấn
            var sqlQuery = $"SELECT * FROM Department WHERE DepartmentId = @DepartmentId";

            var param = new DynamicParameters();
            param.Add("DepartmentId", departmentId);


            var response = await UnitOfWork.Connection.QuerySingleOrDefaultAsync<Department>(sqlQuery, param, transaction: UnitOfWork.Transaction);

            Console.WriteLine(response);

            if (response != null)
            {
                return true;
            }
            return false;
        }
    }
}
