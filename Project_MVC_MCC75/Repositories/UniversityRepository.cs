using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories.Interface;

namespace Project_MVC_MCC75.Repositories;

public class UniversityRepository : IRepository<int, University>
{
    private readonly MyContext context;

    public UniversityRepository(MyContext context)
    {
        this.context = context;
    }

    public int Delete(int key)
    {
        int result = 0;//menampung data result
        var university = GetById(key);
        if (university == null)
        {
           return result;
        }

        context.Remove(university);
        result = context.SaveChanges();

        return result;
       
    }

    public List<University> GetAll()
    {
        return context.Universities.ToList() ?? null;
    }

    public University GetById(int key)
    {
        return context.Universities.Find(key) ?? null;
    }

    public int Insert(University entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();

        return result;
    }

    public int Update(University entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }
}
