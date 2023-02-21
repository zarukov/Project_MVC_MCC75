using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;

namespace MCC75NET.Controllers;
public class AccountRoleController : Controller
{
    private readonly MyContext context;
    public AccountRoleController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var AccountRoles = context.AccountRoles.ToList();
        return View(AccountRoles);
    }
    public IActionResult Details(int id)
    {
        var accountrole = context.AccountRoles.Find(id);
        return View(accountrole);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AccountRole accountrole)
    {
        context.Add(accountrole);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(int id)
    {
        var accountrole = context.AccountRoles.Find(id);
        return View(accountrole);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AccountRole accountrole)
    {
        context.Entry(accountrole).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var accountrole = context.AccountRoles.Find(id);
        return View(accountrole);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var accountrole = context.AccountRoles.Find(id);
        context.Remove(accountrole);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}