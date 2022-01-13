using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WaServer.Data.Entities;

namespace WaServer.Data.Repositories.Contracts
{
    public interface IOrdersRepository : IBasicRepository<Order>
    {
        Task<IList<Order>> GetByInterval(DateTime from, DateTime to);
    }
}
