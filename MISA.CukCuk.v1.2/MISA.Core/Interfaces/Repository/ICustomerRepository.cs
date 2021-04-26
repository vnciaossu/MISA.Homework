using MISA.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// Created By : TMQuy
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        IEnumerable<Customer> GetAll();

        /// <summary>
        /// Lấy thông tin khách hàng theo ID khách hàng
        /// </summary>
        /// Created By : TMQuy
        /// <param name="customerId">Id khashc hàng</param>
        /// <returns>Thông tin của 1 khách hàng có Id khách hàng : customerId</returns>
        Customer GetCustomerById(Guid customerId);

        /// <summary>
        /// Thêm bản ghi 
        /// Thêm thông tin khách hàng
        /// Số bản ghi được thêm
        /// </summary>
        /// Created By : TMQuy
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns>Số bản ghi  được thêm vào</returns>
        /// 
        int Insert(Customer customer);

        /// <summary>
        /// Sửa thông tin khách hàng
        /// Sô bản ghi bị thay đổi
        /// </summary>
        /// Created By : TMQuy
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns>Số bản ghi bị sửa đổi</returns>
        /// 
        int Update(Customer customer);


        /// <summary>
        /// Xóa thông tin khách hàng
        /// Số bản ghi xóa 
        /// </summary>
        /// Created By : TMQuy
        /// <param name="customerId">Mã khách hàng</param>
        /// <returns>Số bản ghi được xóa</returns>
        int Delete(Guid customerId);

        /// <summary>
        /// Kiểm tra trùng mã
        // Created By : TMQuy
        /// </summary>
        /// <param name="customerCode">Mã khách hàng</param>
        /// <returns>true or false</returns>
        bool CheckCustomerExists(string customerCode);


        /// <summary>
        /// Kiểm tra trùng số điện thoại
        // Created By : TMQuy
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns>true or false</returns>
        bool CheckPhoneNumberExists(string phoneNumber);

        /// <summary>
        /// Lấy mã của khách hàng theo Id khách hàng
        // Created By : TMQuy
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns>Mã khách hàng</returns>
        string GetCustomerCodeById(Guid customerId);


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
