using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Services;
using System;
using System.Linq;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class CustomerGroupController : ControllerBase
    {
        private ICustomerGroupService _customerGroupService;

        public CustomerGroupController(ICustomerGroupService customerGroupService)
        {
            _customerGroupService = customerGroupService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customerGroup = _customerGroupService.GetAll();
            return Ok(customerGroup);
        }

        [HttpPost]
        public IActionResult Post(CustomerGroup customerGroup)
        {
            try
            {
                var res = _customerGroupService.Insert(customerGroup);
                if (res > 0)
                {
                    return StatusCode(201, res);
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

        [HttpDelete("{customerGroupId}")]
        public IActionResult Delete(Guid customerGroupId)
        {
            try
            {
                var res = _customerGroupService.Delete(customerGroupId);
                if (res > 0)
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

        [HttpGet("{customerGroupId}")]
        public IActionResult GetById(Guid customerGroupId)
        {
            try
            {
               var customerGroup = _customerGroupService.GetById(customerGroupId);
                if(customerGroup != null)
                {
                    return Ok(customerGroup);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{customerGroupId}")]
        public IActionResult Put([FromBody] CustomerGroup customerGroup)
        {
            try
            {
                var res  = _customerGroupService.Update(customerGroup);
                if (res > 0)
                {
                    return Ok("Update thành công");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}