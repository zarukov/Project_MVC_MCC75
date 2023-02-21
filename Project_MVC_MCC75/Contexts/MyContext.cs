using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Models;

namespace Project_MVC_MCC75.Contexts;
//membuat model2 yang akan didaftarkan di database
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {

    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Education> Educations { get; set; }    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profiling> Profilings { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<University> Universities { get; set;}

    //pembuatan customisasi constraint/relations/kardinalitas (Fluent API)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>().HasIndex(e => new
        {
            e.Email,
            e.PhoneNumber
        }).IsUnique();
        //membuat atribute menjadi unique
        /* modelBuilder.Entity<Employee>().HasAlternateKey(e => new
         {
             e.Email,
             e.PhoneNumber
         });*/

        //Relasi one Employee ke one Account + menjadi Primary Key
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Account)
            .WithOne(a => a.Employee)
            .HasForeignKey<Account>(fk => fk.EmployeeNIK);
    }
}
