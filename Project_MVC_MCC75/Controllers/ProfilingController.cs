﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;

namespace MCC75NET.Controllers;
public class ProfilingController : Controller
{
    private readonly MyContext context;
    public ProfilingController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var profilings = context.Profilings.ToList();
        return View(profilings);
    }
    public IActionResult Details(int id)
    {
        var profiling = context.Profilings.Find(id);
        return View(profiling);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Profiling profiling)
    {
        context.Add(profiling);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(int id)
    {
        var profiling = context.Profilings.Find(id);
        return View(profiling);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Profiling profiling)
    {
        context.Entry(profiling).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var profiling = context.Profilings.Find(id);
        return View(profiling);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var profiling = context.Profilings.Find(id);
        context.Remove(profiling);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}