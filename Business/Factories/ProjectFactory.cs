using Business.Dtos;
using Business.Models;
using Data.Entities;
using Microsoft.Identity.Client;

namespace Business.Factories;

public class ProjectFactory
{
    //Create
    public static ProjectEntity Create(ProjectRegistrationForm form)
    {
        return new ProjectEntity
        {
            ProjectName = form.ProjectName,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Status = form.Status,
            CustomerId = form.CustomerId,
            ServiceId = form.ServiceId,
            EmployeeId = form.EmployeeId
        };
    }
    //Read

    public static IEnumerable<Project> Create(IEnumerable<ProjectEntity> entities)
    {
        return entities.Select(x => new Project
        {
            Id = x.Id,
            ProjectName = x.ProjectName,
            Description = x.Description,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Status = x.Status,
            CustomerId = x.CustomerId,
            ServiceId = x.ServiceId,
            EmployeeId = x.EmployeeId,
            Customer = new Customer
            {
                Id = x.Customer.Id,
                Name = x.Customer.Name
            },
            Employee = new Employee
            {
                Id = x.Employee.Id,
                FirstName = x.Employee.FirstName,
                LastName = x.Employee.LastName
            },
            Service = new Service
            {
                Id = x.Service.Id,
                ServiceName = x.Service.ServiceName
            }
        });
    }

    //Update
    public static ProjectEntity UpdateEntity(ProjectEntity entity, ProjectUpdateForm form)
    {
        entity.Id = form.Id;
        entity.ProjectName = form.ProjectName;
        entity.Description = form.Description;
        entity.StartDate = form.StartDate;
        entity.EndDate = form.EndDate;
        entity.Status = form.Status;
        entity.CustomerId = form.CustomerId;
        entity.ServiceId = form.ServiceId;
        entity.EmployeeId = form.EmployeeId;
        return entity;
    }
    public static Project Create(ProjectEntity entity) 
    {
        return new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Status = entity.Status,
            CustomerId = entity.CustomerId,
            ServiceId = entity.ServiceId,
            EmployeeId = entity.EmployeeId,
        };
    }
}