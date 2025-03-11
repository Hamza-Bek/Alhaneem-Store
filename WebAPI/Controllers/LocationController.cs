using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> GetLocation()
    {
        var response = await _locationRepository.GetLocationByIdAsync();
        
        return Ok(new ApiResponse<Location>(
            "Location retrieved successfully",
            true,
            response
            ));
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddLocation(Location location)
    {
        var response = await _locationRepository.AddLocationAsync(location);
        
        return Ok(new ApiResponse<Location>(
            "Location added successfully",
            true,
            response
            ));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateLocation(Location location)
    {
        var response = await _locationRepository.UpdateLocationAsync(location);
        
        return Ok(new ApiResponse<Location>(
            "Location updated successfully",
            true,
            response
            ));
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteLocation(Guid id)
    {
        var response = await _locationRepository.DeleteLocationAsync(id);
        
        return Ok(new ApiResponse<bool>(
            "Location deleted successfully",
            true,
            response
            ));
    }
}