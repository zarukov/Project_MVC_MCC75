using Microsoft.AspNetCore.Http;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories.Interface;
using Project_MVC_MCC75.ViewModels;

namespace Project_MVC_MCC75.Repositories;

public class AccountRepository :IRepository<int, Account>
{
    private readonly MyContext context;
    private readonly EmployeeRepository employeeRepository;
    private readonly UniversityRepository universityRepository;

    public AccountRepository(MyContext context, EmployeeRepository employeeRepository, UniversityRepository universityRepository)
	{
        this.context = context;
        this.employeeRepository = employeeRepository;
        this.universityRepository = universityRepository;
    }

    public int Insert(Account entity)
    {
        throw new NotImplementedException();
    }

    public int Delete(int key)
    {
        throw new NotImplementedException();
    }

    public List<Account> GetAll()
    {
        return context.Accounts.ToList() ?? null;
    }

    public Account GetById(int key)
    {
        throw new NotImplementedException();
    }

    public int Register(RegisterVM registerVM)
    {
        int result = 0;
        // Bikin kondisi untuk mengecek apakah data university sudah ada
        University university = new University
        {
            Name = registerVM.UniversityName
        };
        if (context.Universities.Any(u => u.Name == registerVM.UniversityName))
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
        
        return result;
    }
    public bool Login(LoginVM loginVM)
    {
       
        var result = context.Employees.Join(
            context.Accounts,
            e => e.NIK,
            a => a.EmployeeNIK,
            (e, a) => new LoginVM
            {
                Email = e.Email,
                Password = a.Password
            });
    
        return result.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password);
    }

    public UserdataVM GetUserdataVM(string email)
    {
        var userdata = (from e in context.Employees
                       join a in context.Accounts
                       on e.NIK equals a.EmployeeNIK
                       join ar in context.AccountRoles
                       on a.EmployeeNIK equals ar.AccountNIK
                       join r in context.Roles
                       on ar.RoleId equals r.Id
                       where e.Email == email
                       select new UserdataVM
                       {
                           Email = e.Email,
                           FullName = String.Concat(e.FirstName, e.LastName),
                           Role = r.Name
                       }).FirstOrDefault();
        return userdata;
    }

    public int Update(Account entity)
    {
        throw new NotImplementedException();
    }
}
