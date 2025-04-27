using Microsoft.EntityFrameworkCore;
using SemLince_Application.IRepositories;
using SemLince_Application.IServices;
using SemLince_Application.Services;
using SemLince_Infrastructure;
using SemLince_Infrastructure.EntityFramework;
using SemLince_Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register Configuration
ConfigurationManager configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeccion de dependencias
//Migracion de EF
//builder.Services.AddDbContext<CategoryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ,
//       b => b.MigrationsAssembly("SemLince_WebApi")));

builder.Services.AddScoped<SqlConnectionFactory>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICareerService, CareerService>();
builder.Services.AddScoped<ICareerRepository, CareerRepository>();
//builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

// APPLICATION DTO



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
