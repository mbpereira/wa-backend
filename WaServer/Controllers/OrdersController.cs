using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WaServer.Data;
using WaServer.Data.Entities;
using WaServer.Data.Repositories.Contracts;
using WaServer.Helpers;

namespace WaServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly SimpleEcommerceContext _context;
        private readonly IOrdersRepository _orders;

        public OrdersController(SimpleEcommerceContext context, IOrdersRepository orders)
        {
            _context = context;
            _orders = orders;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            try
            {
                await _orders.Create(order);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page = null, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            try
            {
                if (page.HasValue)
                    return Ok(await _orders.GetAll(page.GetSkip(), page.GetTake()));
                return Ok(await _orders.GetByInterval(from.GetValueOrDefault(), to.GetValueOrDefault()));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _orders.FindByKey(id));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Order order)
        {
            try
            {
                await _orders.Update(order);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var order = await _orders.FindByKey(id);
                await _orders.Remove(order);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
