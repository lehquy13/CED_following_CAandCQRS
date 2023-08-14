
// Add services to the container.

using System.Net;
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
app.UseStaticFiles();
app.UseStatusCodePages(async context => {  
    var request = context.HttpContext.Request;  
    var response = context.HttpContext.Response;  
  
    // you may also check requests path to do this only for specific methods         
    // && request.Path.Value.StartsWith("/specificPath")  
    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)  
    {  
        response.Redirect("/Authentication");  //redirect to the Account Controller login page.  
    }  
});
app.UseSession();

app.UseHttpsRedirection();

app.UseAuthentication();

//app.UseMiddleware<GlobalErrorHandlingMiddleWare>();



app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();