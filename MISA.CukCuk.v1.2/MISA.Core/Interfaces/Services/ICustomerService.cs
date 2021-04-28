using MISA.Core.Entity;
using System;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Lấy dữ liệu bảng ghi phân trang và lọc
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Pagging<Customer> GetCustomers(CustomerFilter filter);
    }
}