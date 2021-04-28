using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Services;
using System;
using System.Linq;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase where MISAEntity : class
    {
        private IBaseService<MISAEntity> _baseService;

        public BaseController(IBaseService<MISAEntity> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var entitys = _baseService.GetAll();
            if (entitys.Count() > 0)
            {
                return Ok(entitys);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                var res = _baseService.Insert(entity);
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

        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            try
            {
                var result = _baseService.GetById(entityId);
                if (result != null)
                {
                    return Ok(result);
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

        [HttpDelete("{entityId}")]
        public virtual IActionResult Delete(Guid entityId)
        {
            try
            {
                var res = _baseService.Delete(entityId);
                if (res > 0)
                {
                    return Ok(res);
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

        [HttpPut("{entityId}")]
        public IActionResult Put([FromBody] MISAEntity entity)
        {
            try
            {
                var res = _baseService.Update(entity);
                if (res > 0)
                {
                    return Ok(res);
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