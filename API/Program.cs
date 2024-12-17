using Microsoft.EntityFrameworkCore;
using SignSage.Core.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Set up DbContext using SQL Server and connection string from User Secrets
builder.Services.AddDbContext<SignSageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();  // Maps API controllers

app.Run();
