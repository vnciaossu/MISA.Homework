using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entity
{
    /// <summary>
    /// Nhóm khách hàng
    /// Created By : TMQuy
    /// </summary>
    public class CustomerGroup
    {
        /// <summary>
        /// Id nhóm khách hàng.
        /// Created By : TMQuy
        /// </summary>
        public Guid CustomerGroupId { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// Created By : TMQuy
        /// </summary>
        public string CustomerGroupName { get; set; }

        /// <summary>
        /// Mô tả
        /// Created By : TMQuy
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ngày thêm vào
        /// Created By : TMQuy
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người thêm vào
        /// Created By : TMQuy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa lần cuối
        /// Created By : TMQuy
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa lần cuối
        /// Created By : TMQuy
        /// </summary>
        public string ModifiedBy { get; set; }

    }
}
