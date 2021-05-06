using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Data;
using System.Linq;

namespace MISA.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool CheckCustomerExists(string customerCode)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@customerCode", customerCode);
                dynamicParameters.Add("@customerId", null);
                var res = dbConnection.QueryFirstOrDefault<bool>("Proc_CheckCustomerCodeExists", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        public bool CheckPhoneNumberExists(string phoneNumber)
        {
            //using (dbConnection = new MySqlConnection(connectionString))
            //{
            //    DynamicParameters dynamicParameters = new DynamicParameters();
            //    dynamicParameters.Add("@m_PhoneNumber", phoneNumber);
            //    var sqlCommand = "Proc_CheckPhoneNumberExists";
            //    var res = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            //    return res;
            //}
            return false;
        }

        public Pagging<Customer> GetCustomers(CustomerFilter filter)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                Pagging<Customer> pageNew = new Pagging<Customer>();
                //DynamicParameters dynamicParameters = new DynamicParameters();
                //dynamicParameters.Add("@page", filter.Page);
                //dynamicParameters.Add("@pageSize", filter.PageSize);
                //dynamicParameters.Add("@filter", filter.filter);
                //dynamicParameters.Add("@customerGroupId", filter.CustomerGroupId);

                var sqlCommand = "Proc_PaggingCustomers";
                var customers = dbConnection.Query<Customer>(sqlCommand, param: filter, commandType: CommandType.StoredProcedure);

                //var totalRecord = dbConnection.QueryFirstOrDefault<int>("Proc_GetTotalCustomers", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                var totalPages = 1;
                if (customers.Count() % 10 == 0)
                {
                    totalPages = customers.Count() / 10;
                }
                else
                {
                    totalPages = (customers.Count() / 10) + 1;
                }
                pageNew = new Pagging<Customer>
                {
                    totalRecord = customers.Count(),
                    totalPages = totalPages,
                    pageIndex = filter.Page,
                    data = customers,
                    pageSize = filter.PageSize
                };
                pageNew.data = customers;
                return pageNew;
            }
        }

    }
}