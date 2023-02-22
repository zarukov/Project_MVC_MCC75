using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories.Interface;
using Project_MVC_MCC75.ViewModels;

namespace Project_MVC_MCC75.Repositories;

public class EducationRepository : IRepository<int, Education>
{
    private readonly MyContext context;
    private readonly UniversityRepository universityRepository;

    public EducationRepository(MyContext context, UniversityRepository universityRepository)
    {
        this.context = context;
        this.universityRepository = universityRepository;
    }

    public int Insert(Education entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();

        return result;
    }


    public List<Education> GetAll()
    {
        return context.Educations.ToList() ?? null;
    }
    public List<EducationUniversityVM> GetAllEducationUniversity()
    {
        var result = (from e in GetAll()
                      join u in universityRepository.GetAll()
                      on e.UniversityId equals u.Id
                      select new EducationUniversityVM
                      {
                          Id = e.Id,
                          Degree = e.Degree,
                          GPA = e.GPA,
                          Major = e.Major,
                          UniversityName = u.Name
                      }).ToList();

        return result;
    }

    public Education GetById(int key)
    {
        return context.Educations.Find(key) ?? null;
    }
    public EducationUniversityVM GetByIdEducationUniversity(int key)
    {
        var educations = GetById(key);
        var result = new EducationUniversityVM
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityName = context.Universities.Find(educations.UniversityId).Name
        };
        return result;
    }
    

    public int Update(Education entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }

    public int Delete(int key)
    {
        int result = 0;
        var education = GetById(key);
        if (education == null)
        {
            return result;
        }

        context.Remove(education);
        result = context.SaveChanges();

        return result;
    }
}
