using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantLMS.Data;
using MultiTenantLMS.Models;

namespace MultiTenantLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            var tenantDomain = HttpContext.Items["TenantDomain"]?.ToString();
            var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Domain == tenantDomain);
            if (tenant == null) return NotFound("Tenant not found");

            var userIds = _context.Users.Where(u => u.TenantId == tenant.Id).Select(u => u.Id);
            var courseIds = _context.Courses.Where(c => c.TenantId == tenant.Id).Select(c => c.Id);

            return await _context.Enrollments
                                 .Where(e => userIds.Contains(e.UserId) && courseIds.Contains(e.CourseId))
                                 .ToListAsync();
        }

        // POST: api/Enrollment
        [HttpPost]
        public async Task<ActionResult<Enrollment>> CreateEnrollment(Enrollment enrollment)
        {
            var tenantDomain = HttpContext.Items["TenantDomain"]?.ToString();
            var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Domain == tenantDomain);
            if (tenant == null) return BadRequest("Tenant not found");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == enrollment.UserId && u.TenantId == tenant.Id);
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == enrollment.CourseId && c.TenantId == tenant.Id);

            if (user == null || course == null)
                return BadRequest("User or Course does not belong to this tenant");

            enrollment.EnrolledAt = DateTime.UtcNow;
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEnrollments), new { id = enrollment.Id }, enrollment);
        }
    }
}
