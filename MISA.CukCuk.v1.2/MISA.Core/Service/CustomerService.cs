﻿using MISA.Core.Entity;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

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
            }
        }

        public Pagging<Customer> GetCustomers(int pageIndex, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Pagging(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}