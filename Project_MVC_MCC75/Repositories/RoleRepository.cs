using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories.Interface;

namespace Project_MVC_MCC75.Repositories;

public class RoleRepository : IRepository<int, Role>
{
    private readonly MyContext context;
	public RoleRepository(MyContext context)
	{
        this.context = context;
    }
    public int Insert(Role entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();

        return result;
    }
    public List<Role> GetAll()
    {
        return context.Roles.ToList() ?? null;
    }

    public Role GetById(int key)
    {
        return context.Roles.Find(key) ?? null;
    }
    public int Update(Role entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }
    public int Delete(int key)
    {
        int result = 0;
        var role = GetById(key);
        if (role == null)
        {
            return result;
        }
        context.Remove(role);
        result = context.SaveChanges();

        return result;
    }

}
