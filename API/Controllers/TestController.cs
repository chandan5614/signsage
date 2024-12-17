using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignSage.Core.Context;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly SignSageDbContext _context;

        public TestController(SignSageDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                // Try to query a simple entity, e.g., Users table
                var testUser = await _context.Users.FirstOrDefaultAsync();
                
                // If successful, return a 200 OK response
                return Ok(new { Message = "Connection successful", User = testUser });
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 Internal Server Error
                return StatusCode(500, new { Message = "Connection failed", Error = ex.Message });
            }
        }
    }
}