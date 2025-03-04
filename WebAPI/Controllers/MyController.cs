using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MyController : Controller
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("API is working!");
    }
}