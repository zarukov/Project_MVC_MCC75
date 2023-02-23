using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories.Interface;
using Project_MVC_MCC75.ViewModels;

namespace Project_MVC_MCC75.Repositories;

public class EmployeeRepository : IRepository<string, Employee>
{
    private readonly MyContext context;
    public EmployeeRepository(MyContext context)
    {
        this.context = context;
    }

    public int Insert(Employee entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();

        return result;
    }
    
    public List<Employee> GetAll()
    {
        return context.Employees.ToList() ?? null;
    }
    public List<EmployeeVM> GetAllEmployee() 
    {
        var results = (from e in GetAll()
                       select new EmployeeVM
                       {
                           NIK = e.NIK,
                           FirstName = e.FirstName,
                           LastName = e.LastName,
                           BirthDate = e.BirthDate,
                           Gender = (Project_MVC_MCC75.ViewModels.GenderEnum)e.Gender,
                           HiringDate = e.HiringDate,
                           Email = e.Email,
                           PhoneNumber = e.PhoneNumber
                       }).ToList();
        return results;
    }

    public Employee GetById(string key)
    {
        return context.Employees.Find(key) ?? null;
    }
    public EmployeeVM GetByIdEmployee(string key)
    {
        var employees = GetById(key);
        var result = new EmployeeVM
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            BirthDate = employees.BirthDate,
            Gender = (Project_MVC_MCC75.ViewModels.GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber
        };
        return result;
    }

    public int Update(Employee entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }

    public int Delete(string key)
    {
        int result = 0;
        var education = GetById(key);
        if (education == null)
        {
            return result;
        }
        context.Remove(education);
        result = context.SaveChanges();

        return result;
    }
}
