using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Repositories;
using Project_MVC_MCC75.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//configure context to sql server database
var connectionString = builder.Configuration.GetConnectionString("Connection");//mengambil koneksi string "Connection" dari appjson
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));//mendaftarkan bahwa kita mendaftarkan mycontext ke connection string

//Dependency Injection
builder.Services.AddScoped<UniversityRepository>();
builder.Services.AddScoped<EducationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
