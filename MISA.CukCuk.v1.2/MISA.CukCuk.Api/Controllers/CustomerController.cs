using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Services;
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

        /// <summary>
        /// Lấy danh sách khách hàng theo từng điều kiện lọc
        /// </summary>
        /// Created By : TMQuy
        /// HTTPStatusCode - 200 : có dữ liệu trả về
        /// HTTPStatusCode - 204 : không có dữ liệu
        /// <returns>Danh sách khách hàng theo điều kiện lọc</returns>
        [HttpGet("Filter")]
        public IActionResult GetCustomers([FromQuery] CustomerFilter filter)

        {
            var paging = _customerService.GetCustomers(filter);

            // Xử lý kết quả trả về cho client.
            if (paging.data.Any())
            {
                return Ok(paging);
            }

            return NoContent();
        }
    }
}