using Microsoft.EntityFrameworkCore;
using Project_MVC_MCC75.Contexts;
using Project_MVC_MCC75.Repositories;
using Project_MVC_MCC75.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//configure context to sql server database
var connectionString = builder.Configuration.GetConnectionString("Connection");//mengambil koneksi string "Connection" dari appjson
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));//mendaftarkan bahwa kita mendaftarkan mycontext ke connection string

// configure session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
});


//Dependency Injection
builder.Services.AddScoped<UniversityRepository>();
builder.Services.AddScoped<EducationRepository>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<RoleRepository>();


//JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;//tanya
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            //usually, this is app-base url
            ValidateAudience = false,
            /*ValidAudience = builder.Configuration["JWT : Audience"],--> kenapa di comment*/

            //if the JWT was created using web service, then this could be a consumer-base URL
            ValidateIssuer = false,
            /*ValidIssuer = builder.Configuration["JWT : Issuer"],--> kenapa di comment*/

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    app.UseAuthorization();
    if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
    {
        response.Redirect("/Unauthorized");
    }
    else if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
    {
        response.Redirect("/Forbidden");
    }
});

app.UseSession();
app.Use(async (context, next) =>
{
    var jwtoken = context.Session.GetString("jwtoken");
    if (!string.IsNullOrEmpty(jwtoken))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + jwtoken);
    }
    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
