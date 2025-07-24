using AuthApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var apps = await _context.Applications.ToListAsync();
            return Ok(apps);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] Application app)
        {
            app.Status = "submitted";
            app.Date = DateTime.UtcNow;
            _context.Applications.Add(app);
            await _context.SaveChangesAsync();
            return Ok(app);
        }
    }

}
