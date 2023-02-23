using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories;
using Project_MVC_MCC75.ViewModels;

namespace MCC75NET.Controllers;
public class AccountController : Controller
{
    private readonly MyContext context;
    private readonly AccountRepository accountRepository;

    public AccountController(MyContext context, AccountRepository accountRepository)
    {
        this.context = context;
        this.accountRepository = accountRepository;
    }
    public IActionResult Index()
    {
        var result = accountRepository.GetAll();
        return View(result);
    }
    public IActionResult Details(string NIK)
    {
        var account = context.Accounts.Find(NIK);
        return View(account);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Account account)
    {
        context.Add(account);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(string NIK)
    {
        var account = context.Accounts.Find(NIK);
        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Account account)
    {
        context.Entry(account).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(string NIK)
    {
        var account = context.Accounts.Find(NIK);
        return View(account);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string NIK)
    {
        var account = context.Accounts.Find(NIK);
        context.Remove(account);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }



    // GET : Account/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST : Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVM registerVM)
    {
        if (ModelState.IsValid)
        {
            // Bikin kondisi untuk mengecek apakah data university sudah ada
            var result = accountRepository.Register(registerVM);
            if(result == 0)
            {
                return RedirectToAction("Index", "Home");
            } 
        }
        return View();
    }

    //GET Login 
    public IActionResult Login()
    {
        return View();
    }

    //POST login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVM loginVM)
    {
        if (accountRepository.Login(loginVM))
        {
            var userdata = accountRepository.GetUserdataVM(loginVM.Email);

            HttpContext.Session.SetString("email", userdata.Email);
            HttpContext.Session.SetString("fullname", userdata.FullName);
            HttpContext.Session.SetString("role", userdata.Role);

            return RedirectToAction("Index","Home");
        }
        ModelState.AddModelError(string.Empty, "Email or Password Not Found. Please Try Again.");
        return View();
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index), "Home");
    }
}

