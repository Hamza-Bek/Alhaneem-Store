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

    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetLocation(string sessionId)
    {
        var response = await _locationRepository.GetLocationBySessionAsync(sessionId);

        if (response == null)
            return NotFound(new ApiResponse<string>(
                "Location not found",
                false,
                null
            ));

        return Ok(new ApiResponse<LocationDto>(
            "Location retrieved successfully",
            true,
            response.ToDto()
        ));
    }

    [HttpPost]
    public async Task<IActionResult> AddLocation([FromBody] LocationDto dto)
    {
        var result = await _locationRepository.AddLocationAsync(dto.ToModel(), dto.SessionId!);
        return Ok(result.ToDto());
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateLocation(Location location, string sessionId)
    {
        var response = await _locationRepository.UpdateLocationAsync(location, sessionId);
        
        return Ok(new ApiResponse<Location>(
            "Location updated successfully",
            true,
            response
            ));
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteLocation(Guid locationId, string sessionId)
    {
        var response = await _locationRepository.DeleteLocationAsync(locationId, sessionId);
        
        return Ok(new ApiResponse<bool>(
            "Location deleted successfully",
            true,
            response
            ));
    }
}