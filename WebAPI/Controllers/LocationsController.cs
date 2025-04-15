using Application.Dtos.Order;
using Application.Interfaces;
using Application.Mappers;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public LocationsController(ILocationRepository locationRepository)
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
    public async Task<IActionResult> AddLocation(LocationDto location, string sessionId)
    {
        var response = await _locationRepository.AddLocationAsync(location.ToModel(), sessionId);
        
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