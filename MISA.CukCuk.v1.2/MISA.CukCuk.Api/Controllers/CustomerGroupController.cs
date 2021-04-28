using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entity;
using MISA.Core.Interfaces.Services;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class CustomerGroupController : BaseController<CustomerGroup>
    {
        private ICustomerGroupService _customerGroupService;

        public CustomerGroupController(ICustomerGroupService customerGroupService) : base(customerGroupService)
        {
            _customerGroupService = customerGroupService;
        }
    }
}