using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationWebApi.Controllers;

[ApiController]
[Route("api/project")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]
    public async Task<IActionResult> CreateProject(ProjectRegistrationForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _projectService.CreateProject(form);
        return result ? Ok() : Problem();
    }
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _projectService.GetAllProjects();
        if (projects == null)
        {
            return NotFound();
        }
        return Ok(projects);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectUpdateForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _projectService.UpdateProject(form);
        return result != null ? Ok(result) : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        var result = await _projectService.DeleteProject(id);
        return result ? Ok() : NotFound();
    }
}
