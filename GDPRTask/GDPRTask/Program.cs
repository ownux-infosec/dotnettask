using GDPRTask.Data.Repository;
using GDPRTask.Service.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register the UserRepository and UserService
builder.Services.AddScoped<UserRepository>(); // Register UserRepository
builder.Services.AddScoped<IUserService, UserService>(); // Register UserService

// Add CORS policy to allow any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()   // Allow requests from any origin
                  .AllowAnyMethod()   // Allow any HTTP method (GET, POST, etc.)
                  .AllowAnyHeader();  // Allow any header
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Add global exception handler middleware
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error != null)
        {
            // Log the exception (you could log to a file, DB, etc.)
            var exception = exceptionHandlerPathFeature.Error;

            // Create a generic error response
            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                error = "An unexpected error occurred. Please try again later."
            });

            await context.Response.WriteAsync(result);
        }
    });
});

app.Run();
