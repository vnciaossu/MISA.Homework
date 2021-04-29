using System;

namespace MISA.Core.Exceptions
{
    public class CustomerException : Exception
    {
        public CustomerException(string msg) : base(msg)
        {
        }

        public CustomerException(string msg, string customerCode) : base(msg)
        {
        }
    }
}