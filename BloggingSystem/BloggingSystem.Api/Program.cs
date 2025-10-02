using BloggingSystem.AppServices.Facades;
using BloggingSystem.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default");
if (!string.IsNullOrWhiteSpace(connectionString))
    builder.Services.AddInfrastructure(connectionString);
else
    builder.Services.AddInfrastructureInMemory("blogdb-dev");
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<BloggingSystem.Api.Middleware.ErrorHandlingMiddleware>();
app.MapControllers();
app.Run();
