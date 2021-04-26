using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Interfaces.Repository;
using MISA.Entity.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        IConfiguration _configuration;
        private string connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration.GetConnectionString("connectionDB");
        }

        /// <summary>
        /// Kiểm tra trùng mã 
        /// </summary>
        /// <param name="customerCode">Mã khách hàng</param>
        /// <returns></returns>
        public bool CheckCustomerExists(string customerCode)
        {
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@m_CustomerCode", customerCode);
            var res = dbConnection.QueryFirstOrDefault<bool>("Proc_CheckCustomerCodeExists", dynamicParameters, commandType: CommandType.StoredProcedure);
            return res;
        }


        /// <summary>
        /// Check trùng số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns></returns>
        public bool CheckPhoneNumberExists(string phoneNumber)
        {
            // Khởi tạo kết nối 

            // Check dữ liệu

            return false;
        }

        /// <summary>
        /// Xóa khách hàng 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int Delete(Guid customerId)
        {
            //1. Khởi tạo kết nối với Database 

            //2. Kết nối Database
            var dbConnection = new MySqlConnection(connectionString);

            // 3. Thực thi với Database (xóa)
            var sqlCommand = "Proc_DeleteCustomer";
            var rowsAffects = dbConnection.Execute(sqlCommand, param: new { CustomerId = customerId }, commandType: CommandType.StoredProcedure);
            return rowsAffects;
        }

        /// <summary>
        ///  Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAll()
        {
            // Khởi tạo kết nối

            // Kết nối với Database 
            var dbConnection = new MySqlConnection(connectionString);

            //Thực thi với DB 
            var sqlCommand = "Proc_GetCustomers";
            var customers = dbConnection.Query<Customer>(sqlCommand, commandType: CommandType.StoredProcedure);
            return customers;
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo id khách hàng
        /// </summary>
        /// <param name="customerId">ID khách hàng</param>
        /// <returns>Thông tin khách hàng</returns>
        public Customer GetCustomerById(Guid customerId)
        {
            //1. Khởi tạo kết nối với Database 

            //2. Kết nối DB
            var dbConnection = new MySqlConnection(connectionString);

            //3. Thực thi với DB (Thêm , Sửa xóa)
            var sqlCommand = $"Proc_GetCustomerById";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@CustomerId", customerId);
            var customer = dbConnection.Query<Customer>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return customer;
        }


        /// <summary>
        /// Lấy mã khách hàng theo ID khách hàng
        /// </summary>
        /// <param name="customerId">ID khách hàng</param>
        /// <returns>Mã khách hàng</returns>
        public string GetCustomerCodeById(Guid customerId)
        {
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            var res = dbConnection.ExecuteScalar<string>($"SELECT c.CustomerCode FROM Customer c WHERE c.CustomerId = '{customerId}'");
            return res;
        }

        /// <summary>
        /// Thêm bản ghi 
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns>Số bản ghi được thêm</returns>
        public int Insert(Customer customer)
        {
            // Khởi tạo kết nối DB 
            // Thực hiện kết nối với DB 
            var dbConnection = new MySqlConnection(connectionString);

            // số dòng bị thay đổi 
            var rowsAffect = dbConnection.Execute("Proc_InsertCustomer", param: customer, commandType: CommandType.StoredProcedure);
            return rowsAffect;
        }


        /// <summary>
        /// Phân trang 
        /// </summary>
        /// <param name="pageIndex">Vị trí trang hiện tại</param>
        /// <param name="pageSize">Kích thước trang : số đối tượng trong 1 trang</param>
        /// <returns></returns>
        public IEnumerable<Customer> Pagging(int pageIndex, int pageSize)
        {
            // 1. Khai báo thông tin kết nối tới Database:

            // 2. Khởi tạo kết nối:
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3. Tương tác với Database (lấy dữ liệu, sửa dữ liệu, xóa dữ liệu)
            var sqlCommand = "Proc_GetCustomerPaging";
            var param = new
            {
                m_PageIndex = pageIndex,
                m_PageSize = pageSize
            };

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@m_PageIndex", param.m_PageIndex);
            dynamicParameters.Add("@m_PageSize", param.m_PageSize);
            var customers = dbConnection.Query<Customer>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return customers;
        }


        /// <summary>
        /// Số bản ghi được cập nhật
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns></returns>
        public int Update(Customer customer)
        {
            //1. Khởi tạo kết nối với Database 

            //2. Kết nối Database
            var dbConnection = new MySqlConnection(connectionString);
            // Thực thi kết nối 
            var sqlCommand = $"Proc_UpdateCustomer";
            var rowAffects = dbConnection.Execute(sqlCommand, param: customer, commandType: System.Data.CommandType.StoredProcedure);
            return rowAffects;
        }

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="fullName">Họ và tên</param>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <param name="customerGroupId">Id khách hàng</param>
        /// <returns>Dữ liệu phân trang</returns>
        /// 
        public Pagging<Customer> GetCustomers(int pageIndex, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId)
        {
            // 1. Khai báo thông tin kết nối tới Database:
            // 2. Khởi tạo kết nối:
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // Tổng số khách hàng có điều kiện.
            var totalRecord = dbConnection.QueryFirstOrDefault<int>("Proc_H_GetTotalCustomers", new { fullName, phoneNumber, customerGroupId }, commandType: CommandType.StoredProcedure);

            // Tính tổng số trang.
            var totalPages = Math.Ceiling((decimal)totalRecord / pageSize);

            // 3. Tương tác với Database (lấy dữ liệu, sửa dữ liệu, xóa dữ liệu)
            var sqlCommand = "Proc_H_GetCustomers";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@page", pageIndex);
            dynamicParameters.Add("@pageSize", pageSize);
            dynamicParameters.Add("@fullName", fullName);
            dynamicParameters.Add("@phoneNumber", phoneNumber);
            dynamicParameters.Add("@customerGroupId", customerGroupId);

            var customers = dbConnection.Query<Customer>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);

            // Dữ liệu pagging 
            var paging = new Pagging<Customer>()
            {
                totalRecord = totalRecord,
                totalPages = (int)totalPages,
                data = customers,
                pageIndex = pageIndex,
                pageSize = pageSize
            };
            return paging;
        }
    }
}
