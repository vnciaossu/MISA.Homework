using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
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
                dynamicParameters.Add("@m_CustomerCode", customerCode);
                var res = dbConnection.QueryFirstOrDefault<bool>("Proc_CheckCustomerCodeExists", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        public bool CheckPhoneNumberExists(string phoneNumber)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@m_PhoneNumber", phoneNumber);
                var sqlCommand = "Proc_CheckPhoneNumberExists";
                var res = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        public Pagging<Customer> GetCustomers(CustomerFilter filter)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                Pagging<Customer> pageNew = new Pagging<Customer>();

                var sqlCommand = "Proc_PaggingCustomers";
                var customers = dbConnection.Query<Customer>(sqlCommand, param: filter, commandType: CommandType.StoredProcedure);

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@filter", filter.filter);
                dynamicParameters.Add("@customerGroupId", filter.CustomerGroupId);

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