using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories;
using Project_MVC_MCC75.ViewModels;

namespace MCC75NET.Controllers;
public class EducationController : Controller
{
    private readonly EducationRepository educationRepository;
    private readonly UniversityRepository universityRepository;

    public EducationController(EducationRepository educationRepository, UniversityRepository universityRepository)
    {
        //this.context = context;
        this.educationRepository = educationRepository;
        this.universityRepository = universityRepository;
    }
    
    public IActionResult Create()
    {
        var universities = universityRepository.GetAll()
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
    public IActionResult Create(EducationUniversityVM education)
    {
        string addComma = education.GPA.ToString().Insert(1, ",");
        double changeToDouble = Convert.ToDouble(addComma);
        education.GPA = (float) changeToDouble;

        var result = educationRepository.Insert(new Education
        {
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityId = Convert.ToInt16(education.UniversityName)
        });
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }


    public IActionResult Index()
    {
        var results = educationRepository.GetAllEducationUniversity();
        return View(results);
    }
    public IActionResult Details(int id)
    {
        var education = educationRepository.GetByIdEducationUniversity(id);
        return View(education);
    }


    public IActionResult Edit(int id)
    {
        var universities = universityRepository.GetAll()
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
    public IActionResult Edit(EducationUniversityVM educationUnivVM)
    {
        var result = educationRepository.Update(new Education
        {
            Id = educationUnivVM.Id,
            Degree = educationUnivVM.Degree,
            GPA = educationUnivVM.GPA,
            Major = educationUnivVM.Major,
            UniversityId = Convert.ToInt16(educationUnivVM.UniversityName)
        });
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }


    public IActionResult Delete(int id)
    {
        var education = educationRepository.GetByIdEducationUniversity(id);
        return View(education);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var result = educationRepository.Delete(id);
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