using MISA.Core.Entity;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;

namespace MISA.Core.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        protected override void Validate(Customer entity)
        {
            if (entity is Customer)
            {
                var customer = entity as Customer;
                CustomerException.CheckCustomerCodeEmpty(customer.CustomerCode);

                var isExits = _customerRepository.CheckCustomerExists(customer.CustomerCode);
                if (isExits == true)
                {
                    throw new CustomerException("Mã khác hàng đã tồn tại trên hệ thống");
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