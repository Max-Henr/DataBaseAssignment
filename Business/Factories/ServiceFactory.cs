using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ServiceFactory
{
    //Create
    public static ServiceEntity Create(ServiceRegistrationForm form)
    {
        return new ServiceEntity
        {
            ServiceName = form.ServiceName,
            Price = form.Price
        };
    }

    //Read

    public static IEnumerable<Service> Create(IEnumerable<ServiceEntity> entities)
    {
        return entities.Select(x => new Service
        {
            Id = x.Id,
            ServiceName = x.ServiceName,
            Price = x.Price
        });
    }

    //Update

    public static ServiceEntity UpdateEntity(ServiceEntity entity, ServiceUpdateForm form)
    {
        entity.Id = form.Id;
        entity.ServiceName = form.ServiceName;
        entity.Price = form.Price;
        return entity;
    }

    public static Service Create(ServiceEntity entity)
    {
        return new Service
        {
            ServiceName = entity.ServiceName,
            Price = entity.Price
        };
    }
}
