using System.Collections.Generic;

namespace MISA.Core.Entity
{
    /// <summary>
    /// Thông tin phân trang
    /// Created By : TMQuy
    /// </summary>
    /// <typeparam name="MISAEntity"></typeparam>
    public class Pagging<MISAEntity> where MISAEntity : class
    {
        /// <summary>
        /// Tổng số khách hàng
        /// Created By : TMQuy
        /// </summary>
        public int totalRecord { get; set; }

        /// <summary>
        /// Tổng số trang
        /// Created By : TMQuy
        /// </summary>
        public int totalPages { get; set; }

        /// <summary>
        /// Dữ liệu phân trang
        /// Created By : TMQuy
        /// </summary>
        public IEnumerable<MISAEntity> data { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// Created By : TMQuy
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// Kích thước trang : số đối tượng trên 1 trang
        /// Created By : TMQuy
        /// </summary>
        public int pageSize { get; set; }
    }
}