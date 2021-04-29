using MISA.Core.Entity;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;

namespace MISA.Core.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        protected override void CustomValidate(Customer entity)
        {
            if (entity is Customer)
            {
                //Xác định xem property nào sẽ thực hiện validate check bắt buộc nhập

                var customer = entity as Customer;

                var isExits = _customerRepository.CheckCustomerExists(customer.CustomerCode);
                if (isExits == true)
                {
                    throw new Exception("Mã khác hàng đã tồn tại trên hệ thống");
                }
                var isPhoneExits = _customerRepository.CheckPhoneNumberExists(customer.PhoneNumber);
                if (isPhoneExits == true)
                {
                    throw new CustomerException("Số điện thoại đã tồn tại trên hệ thống");
                }
            }
        }

        public Pagging<Customer> GetCustomers(CustomerFilter filter)
        {
            return _customerRepository.GetCustomers(filter);
        }
    }
}