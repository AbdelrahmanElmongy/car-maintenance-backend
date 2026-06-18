using Microsoft.AspNetCore.Mvc;
using CarServiceAPI.Data;

namespace CarServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = new List<object>
    {
        new { UserId = 1, Name = "Menna", Email = "menna@gmail.com" },
        new { UserId = 2, Name = "Sara", Email = "sara@gmail.com" }
    };

            return Ok(customers);
        }
    }
}

