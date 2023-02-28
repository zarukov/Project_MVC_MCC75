using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Handler;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories;
using Project_MVC_MCC75.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MCC75NET.Controllers;

public class AccountController : Controller
{
    private readonly MyContext context;
    private readonly AccountRepository accountRepository;
    private readonly IConfiguration configuration;

    public AccountController(MyContext context, AccountRepository accountRepository, IConfiguration configuration)
    {
        this.context = context;
        this.accountRepository = accountRepository;
        this.configuration = configuration;
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
    public IActionResult Register(RegisterVM registerVM, Hashing hashing)
    {
        if (ModelState.IsValid)
        {
            // Bikin kondisi untuk mengecek apakah data university sudah ada
            var result = accountRepository.Register(registerVM, hashing);
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

            /*HttpContext.Session.SetString("email", userdata.Email);
            HttpContext.Session.SetString("fullname", userdata.FullName);
            HttpContext.Session.SetString("role", userdata.Role);*/
            var roles = accountRepository.GetRolesByNIK(loginVM.Email);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.FullName)
            };

            foreach(var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signIn
                );

            var generateToken =  new JwtSecurityTokenHandler().WriteToken(token);

            HttpContext.Session.SetString("jwtoken", generateToken);
            return RedirectToAction(nameof(Index),"Home");
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

