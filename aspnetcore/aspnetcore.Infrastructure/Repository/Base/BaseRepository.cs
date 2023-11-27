using Dapper;
using aspnetcore.Application;
using aspnetcore.Domain;
using aspnetcore.Domain.Resource;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace aspnetcore.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IEntity
    {
        #region Fields
        protected readonly IUnitOfWork UnitOfWork;

        public virtual string TableName { get; set; } = typeof(TEntity).Name;

        #endregion

        #region Constructor
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<TEntity?> FindAsync(Guid id)
        {
            // Tạo câu truy vấn
            var sqlQuery = $"SELECT * FROM {TableName} WHERE {TableName}Id = @id";

            var param = new DynamicParameters();
            param.Add("id", id);

            var response = await UnitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(sqlQuery, param, transaction: UnitOfWork.Transaction
                );

            return response;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            // Tạo câu truy vấn
            var sqlQuery = $"CALL Proc_{TableName}_GetAll{TableName}";

            var response = await UnitOfWork.Connection.QueryAsync<TEntity>(sqlQuery, transaction: UnitOfWork.Transaction);

            return response.ToList();
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);

            if(entity == null)
            {
                throw new NotFoundException(ResourceVN.Error_NotFound, (int)StatusCode.NotFound);
            }

            return entity;
        }

        public async Task<(List<TEntity>, List<Guid>)> GetListIdsAsync(List<Guid> ids)
        {
            var retrievedEntities = new List<TEntity>();
            var remainingIds = new List<Guid>();

            var sqlQuery = $"SELECT * FROM {TableName} WHERE {TableName}Id IN @ids";

            var param = new DynamicParameters();
            param.Add("ids", ids);

            var response = await UnitOfWork.Connection.QueryAsync<TEntity>(sqlQuery, param);

            retrievedEntities = response.ToList();
            remainingIds = ids.Except(retrievedEntities.Select(entity => entity.GetId())).ToList();

            return (retrievedEntities, remainingIds);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var (columns, values, _) = GetColumnsAndValues();

            // Tạo câu truy vấn
            var sqlQuery = $"INSERT INTO {TableName} ({columns}) VALUES ({values})";


            await UnitOfWork.Connection.ExecuteAsync(sqlQuery, param: entity, transaction: UnitOfWork.Transaction);

            return entity;
        }

        private static (string? columns, string? values, string? updateColumns) GetColumnsAndValues()
        {
            var properties = typeof(TEntity).GetProperties();

            var propColumns = properties.Where(p => p.Name != "DepartmentName").Select(p => p.Name);
            //var nameColumn = properties.FirstOrDefault(p => p.Name == "FullName");
            //var codeColumn = properties.FirstOrDefault(p => p.Name == "EmployeeCode");
            var columns = string.Join(", ", propColumns);
            //var employeeName = nameColumn?.Name;
            //var employeeCode = codeColumn?.Name;


            var propValues = properties.Where(p => p.Name != "DepartmentName").Select(p => $"@{p.Name}");
            var values = string.Join(", ", propValues);

            var propUpdateColumns = properties.Where(p => p.Name != "DepartmentName").Select(p => $"{p.Name} = @{p.Name}");
            var updateColumns = string.Join(", ", propUpdateColumns);

            return (columns, values, updateColumns);
        }


        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var (_, _, updateColumns) = GetColumnsAndValues();
            var sqlQuery = $"UPDATE {TableName} SET {updateColumns} WHERE {TableName}Id = @{TableName}Id";

            await UnitOfWork.Connection.ExecuteAsync(sqlQuery, param: entity);

            return entity;
        }
        public async Task<int> DeleteAsync(TEntity entity)
        {
            // Tạo câu truy vấn
            var sqlQuery = $"DELETE FROM {TableName} WHERE {TableName}Id = @id";

            var param = new DynamicParameters();
            param.Add("id", entity.GetId());

            var response = await UnitOfWork.Connection.ExecuteAsync(sqlQuery, param, transaction: UnitOfWork.Transaction);


            return response;
        }

        public async Task<int> DeleteManyAsync(List<TEntity> entities)
        {
            // Tạo câu truy vấn
            var sqlQuery = $"DELETE FROM {TableName} WHERE {TableName}Id IN @id";

            var param = new DynamicParameters();
            param.Add("id", entities.Select(entity => entity.GetId()));

            var response = await UnitOfWork.Connection.ExecuteAsync(sqlQuery, param);

            return response;
        }
        #endregion
    }
}
