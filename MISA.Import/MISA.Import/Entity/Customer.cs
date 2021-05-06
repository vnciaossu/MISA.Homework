using System;

namespace MISA.Import.Entity
{
    public class Customer
    {
        public Customer(string fullName, string customerCode, string memberCardCode, string customerGroupName, Guid? customerGroupId, string phoneNumber, DateTime? dateOfBirth, string companyName, string taxCode, string email, string address, string note, string status)
        {
           
            FullName = fullName;
            CustomerCode = customerCode;
            MemberCardCode = memberCardCode;
            CustomerGroupName = customerGroupName;
            CustomerGroupId = customerGroupId;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            CompanyName = companyName;
            TaxCode = taxCode;
            Email = email;
            Address = address;
            Note = note;
            Status = status;
        }

        private Guid CustomerId { get; set; }
        public string FullName { get; set; }
        public string CustomerCode { get; set; }
        public string MemberCardCode { get; set; }
        public string CustomerGroupName { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CompanyName { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
