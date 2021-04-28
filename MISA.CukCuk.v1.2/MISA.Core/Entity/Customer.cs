using System;

namespace MISA.Core.Entity
{
    /// <summary>
    /// Thông tin khách khách
    /// Created By : TMQuy
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Khóa chính
        /// Created By : TMQuy
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// Created By : TMQuy
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// Created By : TMQuy
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// Created By : TMQuy
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// Created By : TMQuy
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Mã thẻ thành viên
        /// Created By : TMQuy
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Mã nhóm khách hàng
        /// Created By : TMQuy
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Số điện thoại
        /// Created By : TMQuy
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// Created By : TMQuy
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Tên công ty của khách hàng
        /// Created By : TMQuy
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã tax công ty của khách hàng
        /// Created By : TMQuy
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Địa chỉ khách hàng
        /// Created By : TMQuy
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ghi chú
        /// Created By : TMQuy
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Ngày tạo
        /// Created By : TMQuy
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// Created By : TMQuy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa lần cuối
        /// Created By : TMQuy
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa lần cuối
        /// Created By : TMQuy
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}