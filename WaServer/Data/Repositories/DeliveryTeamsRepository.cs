using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaServer.Data.Entities;
using WaServer.Data.Repositories.Contracts;

namespace WaServer.Data.Repositories
{
    public class DeliveryTeamsRepository : IBasicRepository<DeliveryTeam>
    {
        private readonly SimpleEcommerceContext _context;

        public DeliveryTeamsRepository(SimpleEcommerceContext context)
        {
            _context = context;
        }

        public async Task Create(DeliveryTeam data)
        {
            await _context.DeliveryTeams.AddAsync(data);
        }

        public async Task<DeliveryTeam> FindByKey(params object[] key)
        {
            return await _context.DeliveryTeams.FindAsync(key);
        }

        public async Task<IList<DeliveryTeam>> GetAll(int? skip = null, int? take = null)
        {
            var query = _context.DeliveryTeams;
            if (skip.HasValue) query.Skip(skip.Value);
            if (take.HasValue) query.Take(take.Value);

            return await query.ToListAsync();
        }

        public Task Remove(DeliveryTeam data)
        {
            return Task.Run(() => _context.Remove(data));
        }

        public Task Update(DeliveryTeam data)
        {
            return Task.Run(() => _context.Update(data));
        }
    }
}
