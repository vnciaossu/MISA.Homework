using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Services;
using System;
using System.Linq;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class CustomerController : BaseController<Customer>
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }

        
        [HttpGet("Pagging")]
        public IActionResult GetPaging(int pageIndex, int pageSize)
        {
            var customers = _customerService.Pagging(pageIndex, pageSize);
            // 4. Kiểm tra dữ liệu và trả về cho Client
            // - Nếu có dữ liệu thì trả về 200 kèm theo dữ liệu
            // - Không có dữ liệu thì trả về 204:
            if (customers.Count() > 0)
            {
                return Ok(customers);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Lấy danh sách khách hàng theo từng điều kiện lọc
        /// </summary>
        /// Created By : TMQuy
        /// <param name="page">Trang hiện tại</param>
        /// <param name="pageSize">Kích thước trang :  số khách hàng được lọc trong 1 trang</param>
        /// <param name="fullName">Họ và tên</param>
        /// <param name="phoneNumber"><Số điện thoại/param>
        /// <param name="customerGroupId">ID nhóm khách hàng</param>
        /// HTTPStatusCode - 200 : có dữ liệu trả về
        /// HTTPStatusCode - 204 : không có dữ liệu
        /// <returns>Danh sách khách hàng theo điều kiện lọc</returns>
        [HttpGet("Filter")]
        public IActionResult GetCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string fullName = "", [FromQuery] string phoneNumber = "", [FromQuery] Guid? customerGroupId = null)
        {
            var paging = _customerService.GetCustomers(page, pageSize, fullName, phoneNumber, customerGroupId);

            // Xử lý kết quả trả về cho client.
            if (paging.data.Any())
            {
                return Ok(paging);
            }

            return NoContent();
        }
    }
}