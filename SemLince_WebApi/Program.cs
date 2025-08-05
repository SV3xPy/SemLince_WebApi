using SemLince_Application.IServices;
using SemLince_Application.Services;
using Scalar.AspNetCore;
using SemLince_Infrastructure.Container;

var builder = WebApplication.CreateBuilder(args);

// Register Configuration
ConfigurationManager configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

//Injeccion de dependencias
//Migracion de EF
//builder.Services.AddDbContext<CategoryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ,
//       b => b.MigrationsAssembly("SemLince_WebApi")));

InfrastructureConfigureServiceContainer.AddRepositories(builder.Services);

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ICareerService, CareerService>();

builder.Services.AddScoped<IBuildingService, BuildingService>();

builder.Services.AddScoped<ILocationService, LocationService>();

builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<IPersonaService, PersonaService>();

//builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

// APPLICATION DTO



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
