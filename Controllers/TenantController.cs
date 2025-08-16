using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantLMS.Data;
using MultiTenantLMS.DTOs;
using MultiTenantLMS.Models;

namespace MultiTenantLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TenantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tenant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDTO>>> GetTenants()
        {
            var tenants = await _context.Tenants.ToListAsync();
            var tenantDTOs = tenants.Select(t => new TenantDTO
            {
                Id = t.Id,
                Name = t.Name,
                Domain = t.Domain
            }).ToList();
            return Ok(tenantDTOs);
        }

        // GET: api/Tenant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TenantDTO>> GetTenant(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null) return NotFound("Tenant not found");

            var tenantDTO = new TenantDTO
            {
                Id = tenant.Id,
                Name = tenant.Name,
                Domain = tenant.Domain
            };
            return Ok(tenantDTO);
        }

        // POST: api/Tenant
        [HttpPost]
        public async Task<ActionResult<TenantDTO>> CreateTenant([FromBody] TenantCreateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Domain))
                return BadRequest("Name and Domain are required.");

            var tenant = new Tenant
            {
                Name = dto.Name,
                Domain = dto.Domain
            };
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            var tenantDTO = new TenantDTO
            {
                Id = tenant.Id,
                Name = tenant.Name,
                Domain = tenant.Domain
            };
            return CreatedAtAction(nameof(GetTenant), new { id = tenant.Id }, tenantDTO);
        }
    }
}
