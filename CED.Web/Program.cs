using CED.Application;
using CED.Infrastructure;
using CED.Infrastructure.Entity_Framework_Core;
using CED.Infrastructure.Entity_Framework_Core.DataSeed;
using CED.Infrastructure.Persistence;
using CED.Web;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();

    builder.Services
         .AddInfrastructure(builder.Configuration)
         .AddApplication()
         .AddPresentation();
}

// Add services to the container.
var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        //app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseRouting();

    app.UseAuthorization();

    app.UseSession();

    app.UseExceptionHandler("/Home/Error");

    app.UseStaticFiles();

    
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    //app.MapControllerRoute(
    //    name: "auth",
    //    pattern: "{controller=Authentication}/{action=Index}/{id?}");

    app.Run();

}


