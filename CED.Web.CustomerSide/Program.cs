
// Add services to the container.

using CED.Application;
using CED.Infrastructure;
using CED.Web.CustomerSide;
using CED.Web.CustomerSide.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();

    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication()
        .AddPresentation();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }
app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();

app.UseAuthentication();

//app.UseMiddleware<GlobalErrorHandlingMiddleWare>();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();