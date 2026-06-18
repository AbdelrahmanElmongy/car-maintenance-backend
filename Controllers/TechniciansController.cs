using Microsoft.AspNetCore.Mvc;
using CarServiceAPI.Data;

namespace CarServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechniciansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TechniciansController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/technicians

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = new List<object>
    {
        new { TechnicianId = 1, Name = "Ahmed", Status = "available", Specialization = "Engine" },
        new { TechnicianId = 2, Name = "Ali", Status = "busy", Specialization = "Oil Change" }
    };

            return Ok(data);
        }

        // GET /api/technicians/available
        [HttpGet("available")]
        public IActionResult GetAvailable()
        {
            var data = new List<object>
    {
        new { TechnicianId = 1, Name = "Ahmed", Status = "available", Specialization = "Engine" }
    };

            return Ok(data);
        }
    }
}
