using MISA.Core.Entity;
using System;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        bool CheckCustomerExists(string customerCode);

        /// <summary>
        /// Kiểm tra trùng số điện thoại
        // Created By : TMQuy
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns>true or false</returns>
        bool CheckPhoneNumberExists(string phoneNumber);

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// Created By : TMQuy
        /// <returns>Dữ liệu phân trang</returns>
        Pagging<Customer> GetCustomers(CustomerFilter filter);
    }
}