using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class RoleFactory
{
    //Create
    public static RoleEntity Create(RoleRegistrationForm form)
    {
        return new RoleEntity
        {
            RoleName = form.RoleName
        };
    }

    //Read

    public static IEnumerable<Role> Create(IEnumerable<RoleEntity> entities)
    {
        return entities.Select(x => new Role
        {
            Id = x.Id,
            RoleName = x.RoleName
        });
    }

    //Update

    public static RoleEntity UpdateEntity(RoleEntity entity, RoleUpdateForm form)
    {
        entity.Id = form.Id;
        entity.RoleName = form.RoleName;
        return entity;
    }

    public static Role Create(RoleEntity entity)
    {
        return new Role
        {
            RoleName = entity.RoleName
        };
    }
}
