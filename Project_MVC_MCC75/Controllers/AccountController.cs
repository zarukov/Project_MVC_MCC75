using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.ViewModels;

namespace MCC75NET.Controllers;
public class AccountController : Controller
{
    private readonly MyContext context;
    public AccountController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var Accounts = context.Accounts.ToList();
        return View(Accounts);
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
            University university = new University
            {
                Name = registerVM.UniversityName
            };
            if(context.Universities.Any(u => u.Name == registerVM.UniversityName))
            {
                university.Id = context.Universities.
                    FirstOrDefault(u => u.Name == university.Name).
                    Id;
            }
            else 
            {
                context.Universities.Add(university);
                context.SaveChanges();
            }


            Education education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = university.Id
            };
            context.Educations.Add(education);
            context.SaveChanges();

            Employee employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = (Project_MVC_MCC75.Models.GenderEnum)registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            Account account = new Account
            {
                EmployeeNIK = registerVM.NIK,
                Password = registerVM.Password
            };
            context.Accounts.Add(account);
            context.SaveChanges();

            AccountRole accountRole = new AccountRole
            {
                AccountNIK = registerVM.NIK,
                RoleId = 2
            };

            context.AccountRoles.Add(accountRole);
            context.SaveChanges();

            Profiling profiling = new Profiling
            {
                EmployeeNIK = registerVM.NIK,
                EducationId = education.Id
            };
            context.Profilings.Add(profiling);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVM loginVM)
    {
        var results = context.Employees.Join(
            context.Accounts,
            e => e.NIK,
            a => a.EmployeeNIK,
            (e, a) => new LoginVM
            {
                Email = e.Email,
                Password = a.Password
            });
        if(results.Any(e => e.Email== loginVM.Email && e.Password == loginVM.Password))
        {
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Email or Password Not Found. Please Try Again.");
        return View();
    }
}

