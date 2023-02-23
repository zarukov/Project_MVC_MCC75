using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories;

namespace MCC75NET.Controllers;
public class RoleController : Controller
{
    private readonly RoleRepository roleRepository;
    public RoleController(RoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Role role)
    {
        var result = roleRepository.Insert(role);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Index()
    {
        var result = roleRepository.GetAll();
        return View(result);
    }
    public IActionResult Details(int id)
    {
        var result = roleRepository.GetById(id);
        return View(result);
    }
    
    public IActionResult Edit(int id)
    {
        var role = roleRepository.GetAll();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Role role)
    {
        var result = roleRepository.Update(role);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var role = roleRepository.GetById(id);
        return View(role);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var result = roleRepository.Delete(id);
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