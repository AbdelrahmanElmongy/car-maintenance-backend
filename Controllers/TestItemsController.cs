using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarMaintenance.Data;

namespace CarMaintenance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestItemsController(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
public async Task<IActionResult> GetAll()
{
    try
    {
        var items = await _context.TestItems.ToListAsync();
        return Ok(items);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.ToString());
    }
}
    }
}