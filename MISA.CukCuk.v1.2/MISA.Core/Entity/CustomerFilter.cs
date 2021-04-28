using System;

namespace MISA.Core.Entity
{
    /// <summary>
    /// Phân trang và lọc khách hàng
    /// Created By : TMQuy
    /// </summary>
    public class CustomerFilter
    {
        /// <summary>
        /// Số thứ tự bản ghi
        /// Created By : TMQuy
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Số lượng bản ghi có trong Page
        /// Created By : TMQuy
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Lọc theo PhoneNumber hay FullName
        /// Created By : TMQuy
        /// </summary>
        public string filter { get; set; } = null;

        /// <summary>
        /// Lọc theo mã ID của nhóm khách hàng
        /// Created By : TMQuy
        /// </summary>
        public Guid? CustomerGroupId { get; set; } = null;
    }
}