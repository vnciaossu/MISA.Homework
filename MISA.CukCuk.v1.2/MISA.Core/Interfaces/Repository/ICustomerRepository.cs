using MISA.Core.Entity;
using System;
using System.Collections.Generic;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        bool CheckCustomerExists(string customerCode);

        /// <summary>
        /// Kiểm tra trùng số điện thoại
        // Created By : TMQuy
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns>true or false</returns>
        bool CheckPhoneNumberExists(string phoneNumber);

        /// <summary>
        /// Phân trang
        /// Lấy danh sách khách hàng theo từng trang
        /// Created By : TMQuy
        /// </summary>
        /// <param name="pageSize">Kích thước trang : số khách hàng trong 1 trang</param>
        /// <param name="pageIndex">Vị trí trang hiện tại</param>
        /// <returns>Danh sách khách hàng</returns>
        IEnumerable<Customer> Pagging(int pageIndex, int pageSize);

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// Created By : TMQuy
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="fullName">Họ và tên</param>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <param name="customerGroupId">Id khách hàng</param>
        /// <returns>Dữ liệu phân trang</returns>
        Pagging<Customer> GetCustomers(int pageIndex, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId);
    }
}