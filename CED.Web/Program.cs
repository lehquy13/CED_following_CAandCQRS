using CED.Application;
using CED.Infrastructure;
using CED.Infrastructure.Entity_Framework_Core;
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
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<CEDDBContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
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


