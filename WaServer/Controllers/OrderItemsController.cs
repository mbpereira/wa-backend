using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WaServer.Data;
using WaServer.Data.Entities;
using WaServer.Data.Repositories.Contracts;

namespace WaServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : BaseController
    {
        private readonly SimpleEcommerceContext _context;
        private readonly IBasicRepository<OrderItem> _orderItems;

        public OrderItemsController(SimpleEcommerceContext context, IBasicRepository<OrderItem> orderItems)
        {
            _context = context;
            _orderItems = orderItems;
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var orderItem = await _orderItems.FindByKey(id);
                await _orderItems.Remove(orderItem);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
