using Microsoft.EntityFrameworkCore;
using PostgreSQL.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Replace "DefaultConnection" with the name of your connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services for controllers
builder.Services.AddControllers();

var app = builder.Build();

// Map API controllers
app.MapControllers(); // This enables attribute routing for your controllers

// You can also keep your default mapping
app.MapGet("/", () => "Real Estate Agency 17");

app.Run();
