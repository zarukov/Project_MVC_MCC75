﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories;

namespace Project_MVC_MCC75.Controllers;
public class UniversityController : Controller
{
    private readonly UniversityRepository universityRepository;

    public UniversityController(UniversityRepository repository)
    {
        this.universityRepository = repository;
    }

    public IActionResult Index()
    {
        var universities = universityRepository.GetAll();
        return View(universities);
    }
    public IActionResult Details(int id)
    {
        var university = universityRepository.GetById(id);
        return View(university);
    }


    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(University university)
    {
        var result = universityRepository.Insert(university);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }


    public IActionResult Edit(int id)
    {
        var university = universityRepository.GetById(id);
        return View(university);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(University university)
    {
        
        var result = universityRepository.Update(university);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }


    public IActionResult Delete(int id)
    {
        var university = universityRepository.GetById(id);
        return View(university);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var result = universityRepository.Delete(id);
        if (result == 0)
        {
            //Data tidak ditemukan
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}