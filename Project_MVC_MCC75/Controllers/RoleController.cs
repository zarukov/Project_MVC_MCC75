using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;

namespace MCC75NET.Controllers;
public class RoleController : Controller
{
    private readonly MyContext context;
    public RoleController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var Roles = context.Roles.ToList();
        return View(Roles);
    }
    public IActionResult Details(int id)
    {
        var role = context.Roles.Find(id);
        return View(role);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Role role)
    {
        context.Add(role);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(int id)
    {
        var role = context.Roles.Find(id);
        return View(role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Role role)
    {
        context.Entry(role).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var role = context.Roles.Find(id);
        return View(role);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var role = context.Roles.Find(id);
        context.Remove(role);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}