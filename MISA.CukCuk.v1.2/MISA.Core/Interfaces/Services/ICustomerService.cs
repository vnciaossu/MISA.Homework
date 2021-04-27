using MISA.Core.Entity;
using System;
using System.Collections.Generic;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerService : IBaseService<Customer>
    {
        IEnumerable<Customer> Pagging(int pageIndex, int pageSize);

        Pagging<Customer> GetCustomers(int pageIndex, int pageSize, string fullName, string phoneNumber, Guid? customerGroupId);
    }
}