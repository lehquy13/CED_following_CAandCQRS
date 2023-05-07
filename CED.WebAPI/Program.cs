using CED.Application;
using CED.Infrastructure;
using CED.Infrastructure.Entity_Framework_Core;
using CED.Infrastructure.Entity_Framework_Core.DataSeed;
using CED.Infrastructure.Persistence;
using CED.WebAPI;
using CED.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication()
        .AddPresentation();
    builder.Services
        .AddControllers(option => option.Filters.Add<ErrorHandlingFilterAttribute>());

    //builder.Services
    //   .AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services
         .AddEndpointsApiExplorer();
    builder.Services
        .AddSwaggerGen();


}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        //app.UseMiddleware<ErrorHandingMiddleware>();

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

    //app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}


  
