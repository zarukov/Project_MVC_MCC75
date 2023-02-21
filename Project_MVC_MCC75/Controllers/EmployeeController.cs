using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.ViewModels;

namespace MCC75NET.Controllers;
public class EmployeeController : Controller
{
    private readonly MyContext context;
    public EmployeeController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var results = context.Employees.Select(e => new EmployeeVM
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
        return View(results);
    }
    public IActionResult Details(string NIK)
    {
        var employee = context.Employees.Find(NIK);
        return View(new EmployeeVM
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = (Project_MVC_MCC75.ViewModels.GenderEnum)employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        });
    }
    public IActionResult Create()
    {
        var employees = context.Employees.ToList();
        /*ViewBag.Gender = employees;*/
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeVM employee)
    {
        context.Add(new Employee
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = (Project_MVC_MCC75.Models.GenderEnum)employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        });
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    //httpget
    public IActionResult Edit(string NIK)
    {
        var employees = context.Employees.ToList();
        ViewBag.Gender = employees;
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EmployeeVM employee)
    {
        context.Entry(new Employee
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = (Project_MVC_MCC75.Models.GenderEnum)employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        }).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(string NIK)
    {
        var employee = context.Employees.Find(NIK);
        return View(new EmployeeVM
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = (Project_MVC_MCC75.ViewModels.GenderEnum)employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string NIK)
    {
        var employee = context.Employees.Find(NIK);
        context.Remove(employee);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
