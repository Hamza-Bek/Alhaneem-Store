using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet("sales/statistics")]
        public async Task<IActionResult> GetSalesStatistics([FromQuery] DateTime? date)
        {
            var salesStatistics = await _adminRepository.GetSalesStatisticsAsync(date ?? default);
            if (salesStatistics == null)
            {
                return NotFound();
            }
            return Ok(salesStatistics);
        }
    }
}
