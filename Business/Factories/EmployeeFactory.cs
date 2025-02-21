using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class EmployeeFactory
{
    //Create

    public static EmployeeEntity Create(EmployeeRegistrationForm form)
    {
        return new EmployeeEntity
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            RoleId = form.RoleId
        };
    }

    //Read

    public static IEnumerable<Employee> Create(IEnumerable<EmployeeEntity> entities)
    {
        return entities.Select(x => new Employee
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            RoleId = x.RoleId,
            Role = new Role
            {
                Id = x.Role.Id,
                RoleName = x.Role.RoleName
            }
        });
    }

    //Update

    public static EmployeeEntity UpdateEntity(EmployeeEntity entity, EmployeeUpdateForm form)
    {
        entity.Id = form.Id;
        entity.FirstName = form.FirstName;
        entity.LastName = form.LastName;
        entity.RoleId = form.RoleId;
        return entity;
    }

    public static Employee Create(EmployeeEntity entity)
    {
        return new Employee
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            RoleId = entity.RoleId
        };
    }
}
