using Microsoft.Extensions.Configuration;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;

namespace MISA.Infrastructure.Repository
{
    public class CustomerGroupRepository : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        public CustomerGroupRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}