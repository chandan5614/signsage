using Microsoft.EntityFrameworkCore;
using SignSage.Core.Context;
using SignSage.API.Middleware;
using SignSage.Infrastructure.Services;
using SignSage.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DbContext with SQL Server and connection string from configuration
builder.Services.AddDbContext<SignSageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Application Services
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IAuditTrailService, AuditTrailService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ISignatureService, SignatureService>();
builder.Services.AddScoped<IRepository<Core.Entities.Document>, Core.Data.Repositories.DocumentRepository>();
builder.Services.AddScoped<IRepository<Core.Entities.User>, Core.Data.Repositories.UserRepository>();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Authentication and Authorization
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options => { /* JWT settings */ });

// Configure logging
builder.Services.AddLogging();

// Configure Cors (if needed)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Enable CORS
app.UseCors("AllowAll");

app.Run();
