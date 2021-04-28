using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity : class
    {
        private IConfiguration _configuration;
        protected string connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration.GetConnectionString("connectionDB");
        }

        //protected string connectionString = "" +
        //    "Host = 47.241.69.179;" +
        //    "Port = 3306;" +
        //    "Database = MF0_NVManh_CukCuk02;" +
        //    "UID = dev;" +
        //    "Password = 12345678;"+
        //    "Convert Zero Datetime=True;";

        protected IDbConnection dbConnection;

        public int Delete(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var tagName = typeof(MISAEntity).Name;
                var sqlCommand = $"Proc_Delete{tagName}";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tagName}Id", entityId);
                var rowsAffects = dbConnection.Execute(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowsAffects;
            }
        }

        public IEnumerable<MISAEntity> GetAll()
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var tagname = typeof(MISAEntity).Name;
                var sqlCommand = $"Proc_Get{tagname}s";
                var customerGroups = dbConnection.Query<MISAEntity>(sqlCommand, commandType: CommandType.StoredProcedure);
                return customerGroups;
            }
        }

        public MISAEntity GetById(Guid entityId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var tagname = typeof(MISAEntity).Name;
                var sqlCommand = $"Proc_Get{tagname}ById";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tagname}Id", entityId);
                var customer = dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return customer;
            }
        }

        public int Insert(MISAEntity entity)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var tagname = typeof(MISAEntity).Name;
                var sqlCommand = $"Proc_Insert{tagname}";
                var rowsAffect = dbConnection.Execute(sqlCommand, param: entity, commandType: CommandType.StoredProcedure);
                return rowsAffect;
            }
        }

        public int Update(MISAEntity entityId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var tagname = typeof(MISAEntity).Name;
                var sqlCommand = $"Proc_Update{tagname}";
                var rowAffects = dbConnection.Execute(sqlCommand, param: entityId, commandType: CommandType.StoredProcedure);
                return rowAffects;
            }
        }
    }
}