using crudGus.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// var dbHost = Environment.GetEnvironmentVariable("MYSQL_HOST");
// var dbPort = Environment.GetEnvironmentVariable("MYSQL_PORT");
// var dbName = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
// var dbUser = Environment.GetEnvironmentVariable("MYSQL_USER");
// var dbPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

// var finalConnectionString = rawConnectionString?
//     .Replace("{MYSQL_DATABASE}", dbName)
//     .Replace("{MYSQL_USER}", dbUser)
//     .Replace("{MYSQL_PASSWORD}", dbPassword)
//     .Replace("{MYSQL_HOST}", dbHost)
//     .Replace("{MYSQL_PORT}", dbPort);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(rawConnectionString, ServerVersion.AutoDetect(rawConnectionString)));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
