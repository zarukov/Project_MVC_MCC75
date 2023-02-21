using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.ViewModels;

namespace MCC75NET.Controllers;
public class EducationController : Controller
{
    private readonly MyContext context;
    public EducationController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var results = context.Educations.Join(
            context.Universities,
            e => e.UniversityId,
            u => u.Id,
            (e, u) => new EducationVM
            {
                Id = e.Id,
                Degree = e.Degree,
                GPA = e.GPA,
                Major = e.Major,
                UniversityName = u.Name
            });
        return View(results);
    }
    public IActionResult Details(int id)
    {
        var education = context.Educations.Find(id);
        return View(new EducationVM
        {
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityName = context.Universities.Find(education.UniversityId).Name
        });
    }
    public IActionResult Create()
    {
        var universities = context.Universities.ToList()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.UniversityName = universities;
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EducationVM education)
    {
        string addComma = education.GPA.ToString().Insert(1, ",");
        double changeToDouble = Convert.ToDouble(addComma);
        education.GPA = (float) changeToDouble;

        context.Add(new Education
        {
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityId = Convert.ToInt16(education.UniversityName)
        });
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(int id)
    {
        var universities = context.Universities.ToList()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.UniversityName = universities;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EducationVM education)
    {
        context.Entry(new Education
        {
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityId = Convert.ToInt16(education.UniversityName)
        }).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var education = context.Educations.Find(id);
        return View(new EducationVM
        {
            
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityName = context.Universities.Find(education.UniversityId).Name
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var education = context.Educations.Find(id);
        context.Remove(education);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}