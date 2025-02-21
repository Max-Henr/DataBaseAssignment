using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationWebApi.Controllers;

[ApiController]
[Route("api/service")]
public class ServiceController(IServiceService serviceService) : ControllerBase
{
    private readonly IServiceService _serviceService = serviceService;

    [HttpPost]
    public async Task<IActionResult> CreateService(ServiceRegistrationForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _serviceService.CreateService(form);
        return result ? Ok() : Problem();
    }
    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var services = await _serviceService.GetAllServices();
        if (services == null)
        {
            return NotFound();
        }
        return Ok(services);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(int id,ServiceUpdateForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _serviceService.UpdateService(form);
        return result != null ? Ok(result) : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        var result = await _serviceService.DeleteService(id);
        return result ? Ok() : NotFound();
    }
}
