using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Services;
using System;
using System.Linq;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Lấy dữ liệu toàn bộ khách hàng
        /// </summary>
        /// <returns>
        /// HttpStatusCode 200 - có dữ liệu trả về
        /// HttpStatusCode 204 - không có dữ liệu
        /// </returns>
        /// Created By : TMQuy
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.GetAll();
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
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Thông tin đối tượng khách hàng</param>
        /// <returns>
        /// 201- thêm mới thành công
        /// 204 - không thêm được vào db
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - có lối xả ra phóa server (exception,...)
        /// </returns>
        /// Created By : TMQuy
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            var res = _customerService.Insert(customer);
            if (res > 0)
            {
                return StatusCode(201, res);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// GET : api/v1/Customer/id
        /// Lấy khách hàng theo id
        /// Created By : TMQuy
        /// </summary>
        /// <param name="id">Id khách hàng</param>
        /// <returns>Đối tượng khách hàng</returns>
        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(Guid customerId)
        {
            var customer = _customerService.GetById(customerId);
            //4. Kiểm tra dữ liệu :
            //- Nếu có dữ liệu trả về 200 kèm theo dữ liệu
            //- Nếu không có dữ liệu trả về 204
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// PUT : api/v1/Customers/id
        /// Sửa thông tin khách hàng
        /// Created By : TMQuy
        /// </summary>
        /// <param name="id">id khách hàng</param>
        /// <param name="customer">Đối tượng khách hàng</param>
        /// <returns>Đối tượng khách hàng sửa</returns>
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Customer customer)
        {
            var rowAffects = _customerService.Update(customer);

            // số bản ghi được sửa đổi
            if (rowAffects > 0)
            {
                return Ok("Sửa thành công");
            }
            return NoContent();
        }

        /// <summary>
        /// DELETE : api/v1/Customers/id
        /// Xóa thông tin khách hàng
        /// </summary>
        /// <param name="id">Id khách hàng</param>
        /// <returns>
        /// 200 - Xóa thành công
        /// 204 - Không xóa được dữ liệu khỏi DB
        /// </returns>
        [HttpDelete("{customerId}")]
        public IActionResult Delete(Guid customerId)
        {
            try
            {
                var rowAffects = _customerService.Delete(customerId);
                if (rowAffects > 0)
                {
                    return Ok("Xóa thành công");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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