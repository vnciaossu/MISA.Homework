using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MISA.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = _customerRepository.GetAll();
            return customers;
        }

        public Customer GetCustomerById(Guid customerId)
        {
            var customer = _customerRepository.GetCustomerById(customerId);
            return customer;
        }

        /// <summary>
        /// Thêm thông tin khách hàng
        /// Created By : TMQuy
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns>Số bản ghi được thêm mới</returns>
        public int Insert(Customer customer)
        {
            // Validate dữ liệu 
            CustomerException.CheckCustomerCodeEmpty(customer.CustomerCode);
            // Check trùng mã 
            var isExists = _customerRepository.CheckCustomerExists(customer.CustomerCode);
            if(isExists == true)
            {
                throw new CustomerException("Mã khách hàng đã tồn tại trong hệ thống !!!!",customer.CustomerCode);
            }
            var rowAffects = _customerRepository.Insert(customer);
            return rowAffects;
        }

        /// <summary>
        /// Sửa thông tin nhân viên
        /// Created By : TMQuy
        /// </summary>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns>Số bản ghi bị thay đổi</returns>
        public int Update(Customer customer)
        {
            // Lấy mã khách hàng thông qua Id khách hàng
            var customerExist = _customerRepository.GetCustomerCodeById(customer.CustomerId);

            // check trùng mã 
            var isExists = _customerRepository.CheckCustomerExists(customer.CustomerCode);
            if (isExists == true && !customer.CustomerCode.Equals(customerExist))
            {
                throw new CustomerException("Mã khách hàng đã tồn tại trong hệ thống !!!!", customer.CustomerCode);
            }
            // số bản ghi bị thay đổi 
            var rowAffects = _customerRepository.Update(customer);
            return rowAffects;
        }
        /// <summary>
        /// Xóa thông tin khách hàng
        /// Created By : TMQuy
        /// </summary>
        /// <param name="customerId">ID khách hàng</param>
        /// <returns>Số bản ghi được xóa bỏ</returns>
        public int Delete(Guid customerId)
        {
            var rowAffects = _customerRepository.Delete(customerId);
            return rowAffects;
        }

        public IEnumerable<Customer> Pagging(int pageIndex, int pageSize)
        {
            var customers = _customerRepository.Pagging(pageIndex, pageSize);
            return customers;
        }

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
        public Pagging<Customer> GetCustomers(int pageIndex, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId)
        {
            var pagging = _customerRepository.GetCustomers(pageIndex, pageSize, fullName, phoneNumber, customerGroupId);
            return pagging;
        }
    }
}
