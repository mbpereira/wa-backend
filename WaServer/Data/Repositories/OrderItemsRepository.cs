using System.Collections.Generic;
using System.Threading.Tasks;
using WaServer.Data.Entities;
using WaServer.Data.Repositories.Contracts;

namespace WaServer.Data.Repositories
{
    public class OrderItemsRepository : IBasicRepository<OrderItem>
    {
        private readonly SimpleEcommerceContext _context;

        public OrderItemsRepository(SimpleEcommerceContext context)
        {
            _context = context;
        }

        public Task Create(OrderItem data)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OrderItem> FindByKey(params object[] key)
        {
            return await _context.OrderItems.FindAsync(key);
        }

        public Task<IList<OrderItem>> GetAll(int? skip = null, int? take = null)
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(OrderItem data)
        {
            return Task.Run(() => _context.OrderItems.Remove(data));
        }

        public Task Update(OrderItem data)
        {
            throw new System.NotImplementedException();
        }
    }
}
