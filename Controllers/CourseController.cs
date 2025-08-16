using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantLMS.Data;
using MultiTenantLMS.DTOs;
using MultiTenantLMS.Models;

namespace MultiTenantLMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();

            var courseDTOs = courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                TenantId = c.TenantId
            }).ToList();

            return Ok(courseDTOs);
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound("Course not found");

            var courseDTO = new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                TenantId = course.TenantId
            };

            return Ok(courseDTO);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<CourseDTO>> CreateCourse([FromBody] CourseCreateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Description))
                return BadRequest("Title and Description are required.");

            var tenant = await _context.Tenants.FindAsync(dto.TenantId);
            if (tenant == null) return BadRequest("Tenant not found with provided TenantId");

            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                TenantId = dto.TenantId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var courseDTO = new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                TenantId = course.TenantId
            };

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, courseDTO);
        }

        // PUT: api/Course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseCreateDTO dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound("Course not found");

            course.Title = dto.Title ?? course.Title;
            course.Description = dto.Description ?? course.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound("Course not found");

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
