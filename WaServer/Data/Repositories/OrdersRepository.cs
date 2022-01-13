using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaServer.Data.Entities;
using WaServer.Data.Repositories.Contracts;

namespace WaServer.Data.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly SimpleEcommerceContext _context;

        public OrdersRepository(SimpleEcommerceContext context)
        {
            _context = context;
        }

        public async Task Create(Order data)
        {
            if (data.CreatedAt.Equals(DateTime.MinValue))
                data.CreatedAt = System.DateTime.Now;
            await _context.Orders.AddAsync(data);
        }

        public async Task<Order> FindByKey(params object[] key)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.DeliveryTeam)
                .FirstOrDefaultAsync(o => o.IdOrder.Equals((int)key[0]));
        }

        public async Task<IList<Order>> GetAll(int? skip = null, int? take = null)
        {
            var query = _context.Orders
                .Include(o => o.Items)
                .Include(o => o.DeliveryTeam)
                .OrderByDescending(o => o.CreatedAt)
                .AsQueryable();
            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IList<Order>> GetByInterval(DateTime from, DateTime to)
        {
            return await _context.Orders
                    .Include(o => o.Items)
                    .Include(o => o.DeliveryTeam)
                    .OrderByDescending(o => o.CreatedAt)
                    .ToListAsync();
        }

        public Task Remove(Order data)
        {
            return Task.Run(() => _context.Orders.Remove(data));
        }

        public async Task Update(Order data)
        {
            foreach (var newItem in data.Items)
                newItem.OrderId = data.IdOrder;

            var oldOrder = await FindByKey(data.IdOrder);

            _context.Entry(oldOrder).CurrentValues.SetValues(data);

            foreach (var oldItem in oldOrder.Items)
            {
                var item = data.Items.FirstOrDefault(o => o.IdOrderItem.Equals(oldItem.IdOrderItem));
                if (item == null)
                    _context.OrderItems.Remove(oldItem);
                else
                    _context.Entry(oldItem).CurrentValues.SetValues(item);
            }

            await _context.OrderItems.AddRangeAsync(data.Items.Where(i => i.IdOrderItem <= 0).ToList());
        }
    }
}
