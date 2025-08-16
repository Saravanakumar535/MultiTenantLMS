using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantLMS.Data;
using MultiTenantLMS.DTOs;
using MultiTenantLMS.Models;

namespace MultiTenantLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var userDTOs = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                TenantId = u.TenantId
            }).ToList();

            return Ok(userDTOs);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("User not found");

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                TenantId = user.TenantId
            };

            return Ok(userDTO);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserCreateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.PasswordHash))
                return BadRequest("Username and PasswordHash are required");

            var tenant = await _context.Tenants.FindAsync(dto.TenantId);
            if (tenant == null) return BadRequest("Tenant not found with provided TenantId");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                TenantId = dto.TenantId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                TenantId = user.TenantId
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDTO);
        }
    }
}
