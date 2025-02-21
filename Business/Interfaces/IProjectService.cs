using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProject(ProjectRegistrationForm form);
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> UpdateProject(ProjectUpdateForm form);
        Task<bool> DeleteProject(int id);
    }
}