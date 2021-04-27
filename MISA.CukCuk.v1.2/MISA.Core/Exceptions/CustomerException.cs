using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class CustomerException:Exception
    {
        public CustomerException(string msg) : base(msg)
        {           
        }
        public CustomerException(string msg, string customerCode) :base(msg)
        {
            
        }
        public static void CheckCustomerCodeEmpty(string customerCode)
        {
            if (string.IsNullOrEmpty(customerCode))
            {       
                throw new CustomerException("Mã khách hàng không được để chống !!!");
            }
        }
        
    }
}
