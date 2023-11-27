using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using aspnetcore.Application;
using aspnetcore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace aspnetcore.Infrastructure
{
    public class SettingRepository : ISettingRepository
    {
        protected readonly IUnitOfWork UnitOfWork;

        #region Constructor
        public SettingRepository(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        #endregion
        public async Task<string> GetFormat()
        {
            var sqlQuery = $"SELECT * FROM Setting";

            var response = await UnitOfWork.Connection.QueryFirstOrDefaultAsync<string>(sqlQuery, transaction: UnitOfWork.Transaction);

            return response;
        }

        public async Task<string> UpdateFormat(string format)
        {
            var sqlQuery = $"UPDATE Setting SET Format = @format";

            var param = new DynamicParameters();
            param.Add("format", format);

            await UnitOfWork.Connection.ExecuteAsync(sqlQuery, param, transaction: UnitOfWork.Transaction);

            return format;
        }
    }
}
