using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    //Create
    public async Task<bool> CreateProject(ProjectRegistrationForm form)
    {
        await _projectRepository.BeginTransactionAsync();
        try
        {
            var project = ProjectFactory.Create(form);
            var result = await _projectRepository.CreateAsync(project);
            await _projectRepository.CommitTransactionAsync();
            await _projectRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Creating Project :: {ex.Message}");
            await _projectRepository.RollbackTransactionAsync();
            return false;
        }
    }

    //Read
    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        var projects = await _projectRepository.GetAllAsync();
        return ProjectFactory.Create(projects);
    }

    //Update
    public async Task<Project> UpdateProject(ProjectUpdateForm form)
    {
        await _projectRepository.BeginTransactionAsync();
        try
        {
            var project = await _projectRepository.GetByIdAsync(x => x.Id == form.Id);
            if (project == null)
                return null!;
            project = ProjectFactory.UpdateEntity(project, form);
            await _projectRepository.UpdateAsync(x => x.Id == form.Id, project);
            if (project != null)
            {
                await _projectRepository.CommitTransactionAsync();
                await _projectRepository.SaveChangesAsync();
                return ProjectFactory.Create(project);
            }
            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating Project :: {ex.Message}");
            await _projectRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    //Delete
    public async Task<bool> DeleteProject(int id)
    {
        await _projectRepository.BeginTransactionAsync();
        try
        {
            var project = await _projectRepository.GetByIdAsync(x => x.Id == id);
            if (project == null)
                return false;
            var result = await _projectRepository.DeleteAsync(x => x.Id == id);
            await _projectRepository.CommitTransactionAsync();
            await _projectRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Deleting Project :: {ex.Message}");
            await _projectRepository.RollbackTransactionAsync();
            return false;
        }
    }
}