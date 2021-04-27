using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MISA.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
       
        public bool CheckCustomerExists(string customerCode)
        {
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@m_CustomerCode", customerCode);
            var res = dbConnection.QueryFirstOrDefault<bool>("Proc_CheckCustomerCodeExists", dynamicParameters, commandType: CommandType.StoredProcedure);
            return res;
        }


        public bool CheckPhoneNumberExists(string phoneNumber)
        {
            // Khởi tạo kết nối

            // Check dữ liệu

            return false;
        }

        public Pagging<Customer> GetCustomers(int pageIndex, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Pagging(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

       
    }
}