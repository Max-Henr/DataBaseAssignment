using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationWebApi.Controllers;

[ApiController]
[Route("api/role")]
public class RoleController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpPost]
    public async Task<IActionResult> CreateRole(RoleRegistrationForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _roleService.CreateRole(form);
        return result ? Ok() : Problem();
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetAllRoles();
        if (roles == null)
        {
            return NotFound();
        }
        return Ok(roles);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id,RoleUpdateForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _roleService.UpdateRole(form);
        return result != null ? Ok(result) : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        var result = await _roleService.DeleteRole(id);
        return result ? Ok() : NotFound();
    }
}
