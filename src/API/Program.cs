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
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Authentication and Authorization
// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Set to true in production for security
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});

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

app.UseAuthentication(); // Use Authentication
app.UseAuthorization();  // Use Authorization

// Map controllers
app.MapControllers();

// Enable CORS
app.UseCors("AllowAll");

app.Run();
