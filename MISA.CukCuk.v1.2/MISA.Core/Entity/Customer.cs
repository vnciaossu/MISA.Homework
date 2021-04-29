using MISA.Core.AttributeCustom;
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
        [MISARequired(name: "Mã khách hàng")]
        [MISAMaxLength(20, msg: "Mã khách hàng không dài quá 20 kí tự")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// Created By : TMQuy
        /// </summary>
        [MISARequired]
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
        /// Tên giới tính
        /// </summary>
        public string GenderName
        {
            get
            {
                if (Gender == 1)
                {
                    return "Nam";
                }
                else if (Gender == 2)
                {
                    return "Nữ";
                }

                return "Không có";
            }
        }

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

        public string CustomerGroupName
        {
            get
            {
                if (CustomerGroupId == Guid.Parse("19165ed7-212e-21c4-0428-030d4265475f"))
                {
                    return "Khách hàng MISA";
                }
                else if (CustomerGroupId == Guid.Parse("2924c34d-68f1-1d0a-c9c7-6c0aeb6ec302"))
                {
                    return "Khách vãng lai";
                }
                else if (CustomerGroupId == Guid.Parse("3631011e-4559-4ad8-b0ad-cb989f2177da"))
                {
                    return "Khách thường";
                }
                else if (CustomerGroupId == Guid.Parse("7a0b757e-41eb-4df6-c6f8-494a84b910f4"))
                {
                    return "Khách VIP";
                }
                return "Không có";
            }
        }

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