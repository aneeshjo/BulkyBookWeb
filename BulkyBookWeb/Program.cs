// Creates the application builder.
// The builder is responsible for configuring services,
// configuration, logging, environment settings, and other
// application-level settings.


using BulkyBook.Business.Services;
using BulkyBook.Business.Services.IServices;
using BulkyBook.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Registers MVC services in the application's Dependency Injection (DI) container.
// This enables ASP.NET Core to use Controllers and Views.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddScoped<ICategoryService, CategoryService>();
// Builds the ASP.NET Core application using all the configuration
// and services registered above.
var app = builder.Build();


// Checks whether the application is running outside the Development environment.
// This block is mainly used to configure production-level error handling
// and security settings.
if (!app.Environment.IsDevelopment())
{
    // Handles unhandled exceptions in Production/Staging environments
    // by directing the user to the Home/Error action.
    app.UseExceptionHandler("/Home/Error");

    // Enables HTTP Strict Transport Security (HSTS).
    // This tells browsers to use HTTPS for future requests to the application.
    app.UseHsts();
}


// Redirects HTTP requests to HTTPS.
// Example:
// http://localhost:5000  →  https://localhost:7000
app.UseHttpsRedirection();


// Enables ASP.NET Core's routing middleware.
// Routing determines which controller/action should handle
// an incoming HTTP request.
app.UseRouting();


// Enables authorization middleware.
// It checks whether the current user has permission to access
// a particular resource when authorization rules are applied.
app.UseAuthorization();


// Enables serving static assets such as CSS, JavaScript,
// images, and other files from the application's static-asset system.
app.MapStaticAssets();


app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


// Defines the default MVC route.
//
// URL pattern:
// /{controller}/{action}/{id?}
//
// controller = Controller name
// action     = Action method
// id         = Optional parameter
//
// Default values:
// controller = Home
// action     = Index
//
// Therefore:
//
// /                    → HomeController → Index()
// /Home                → HomeController → Index()
// /Home/Privacy        → HomeController → Privacy()
// /Product/Details/5   → ProductController → Details(5)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new {area="Customer"})
    .WithStaticAssets();


// Starts the ASP.NET Core application
// and begins listening for incoming HTTP requests.
app.Run();