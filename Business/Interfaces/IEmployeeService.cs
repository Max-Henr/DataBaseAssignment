using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployee(EmployeeRegistrationForm form);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> UpdateEmployee(EmployeeUpdateForm form);
        Task<bool> DeleteEmployee(int id);
    }
}