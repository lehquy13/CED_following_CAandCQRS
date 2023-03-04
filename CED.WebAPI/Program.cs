using CED.Application;
using CED.Infrastructure;
using CED.WebAPI.Filters;
using CED.WebAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    //builder.Services
    //    .AddControllers(option => option.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services
       .AddControllers();
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
        app.UseMiddleware<ErrorHandingMiddleware>();

    }
    else
    {
        app.UseExceptionHandler("/error");

    }


    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
