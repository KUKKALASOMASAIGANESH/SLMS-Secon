using Microsoft.AspNetCore.Mvc;
using SLMS.DAL.Data;
using SLMS.DOL.Entities;

namespace SLMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempTestController : ControllerBase
    {
        private readonly SLMSDbContext _context;

        public TempTestController(SLMSDbContext context)
        {
            _context = context;
        }

        // ✅ POST: Add data
        [HttpPost]
        public IActionResult Add(TempTest temp)
        {
            _context.TempTests.Add(temp);
            _context.SaveChanges();
            return Ok("Added Successfully");
        }

        // ✅ GET: Get all data
        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.TempTests.ToList();
            return Ok(data);
        }
    }
}
