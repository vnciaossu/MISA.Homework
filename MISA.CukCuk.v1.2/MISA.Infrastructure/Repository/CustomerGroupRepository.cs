using Microsoft.Extensions.Configuration;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    public class CustomerGroupRepository : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        /* IConfiguration _configuration;
         private string connectionString;

         public CustomerGroupRepository(IConfiguration configuration)
         {
             _configuration = configuration;
             connectionString = configuration.GetConnectionString("connectionDB");
         }*/


    }
}
