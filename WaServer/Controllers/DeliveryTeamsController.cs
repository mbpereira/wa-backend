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
    public class DeliveryTeamsController : BaseController
    {
        private readonly SimpleEcommerceContext _context;
        private readonly IBasicRepository<DeliveryTeam> _deliveryTeams;

        public DeliveryTeamsController(SimpleEcommerceContext context, IBasicRepository<DeliveryTeam> deliveryTeams)
        {
            _context = context;
            _deliveryTeams = deliveryTeams;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _deliveryTeams.GetAll());
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _deliveryTeams.FindByKey(id));
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
                var deliveryTeam = await _deliveryTeams.FindByKey(id);
                await _deliveryTeams.Remove(deliveryTeam);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(DeliveryTeam team)
        {
            try
            {
                await _deliveryTeams.Update(team);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(DeliveryTeam team)
        {
            try
            {
                await _deliveryTeams.Create(team);
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
