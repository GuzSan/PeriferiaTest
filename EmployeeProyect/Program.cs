using EmployeeProyect.Application.InMemoryRepositories;
using EmployeeProyect.Application.Interfaces;
using EmployeeProyect.Application.Repositories;
using EmployeeProyect.Application.Services;
using EmployeeProyect.Infrastucture.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Load JWT settings correctly
var jwtSettings = new
{
    SecretKey = configuration["JwtSettings:SecretKey"],
    Issuer = configuration["JwtSettings:Issuer"],
    Audience = configuration["JwtSettings:Audience"]
};


// Validate that values are not null (optional for debugging)
if (string.IsNullOrEmpty(jwtSettings.SecretKey))
    throw new ArgumentNullException("JwtSettings:SecretKey is missing in appsettings.json");

var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Ensures token expiration works accurately
        };
    });

// Add authorization
builder.Services.AddAuthorization();

// Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employees API",
        Version = "v1",
        Description = "API for managing employees and departments for the test of Periferi IT Group"
    });



    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<EmployeeDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the repository as a scoped service
// builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeMemoryRepository>();

// Register EmployeeService if it's used
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddSingleton<InMemoryEmployee>();

var app = builder.Build();

/*app.UseAuthentication();  
app.UseAuthorization();*/

app.MapControllers();

// **Enable Swagger in Development**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// **Redirect Root URL to Swagger**
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.Run();
