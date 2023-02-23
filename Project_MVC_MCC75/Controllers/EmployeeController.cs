using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories;
using Project_MVC_MCC75.ViewModels;

namespace MCC75NET.Controllers;
public class EmployeeController : Controller
{
    private readonly EmployeeRepository employeeRepository;

    public EmployeeController(EmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }
    public IActionResult Create()
    {
        var employees = employeeRepository.GetAll();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeVM employee)
    {
        var result = employeeRepository.Insert(new Employee
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
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Index()
    {
        var results = employeeRepository.GetAllEmployee();
        return View(results);
    }
    public IActionResult Details(string NIK)
    {
        var result = employeeRepository.GetByIdEmployee(NIK);
        return View(result);
    }
    
    //httpget
    public IActionResult Edit(string NIK)
    {
        var employees = employeeRepository.GetAll();
        /*ViewBag.Gender = employees;*/
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EmployeeVM employeeVM)
    {
        var result = employeeRepository.Update(new Employee
        {
            NIK = employeeVM.NIK,
            FirstName = employeeVM.FirstName,
            LastName = employeeVM.LastName,
            BirthDate = employeeVM.BirthDate,
            Gender = (Project_MVC_MCC75.Models.GenderEnum)employeeVM.Gender,
            HiringDate = employeeVM.HiringDate,
            Email = employeeVM.Email,
            PhoneNumber = employeeVM.PhoneNumber
        });
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(string NIK)
    {
        var employee = employeeRepository.GetByIdEmployee(NIK);
        return View(employee);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string NIK)
    {
        var result = employeeRepository.Delete(NIK);
        if (result == 0)
        {
            //data tidak ditemukan
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
