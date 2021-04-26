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
                var response = new
                {
                    userMsg = "Mã khách hàng không được phép để chống ",
                    devMsg = "Mã khách hàng không được phép để trống",
                    MISACode = "001"
                };
                throw new CustomerException("Mã khách hàng không được để chống !!!");
            }
        }
        
    }
}
