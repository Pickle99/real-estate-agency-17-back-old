using Microsoft.EntityFrameworkCore;
using PostgreSQL.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services for controllers
builder.Services.AddControllers();

var app = builder.Build();

// Map API controllers
app.MapControllers(); 

app.MapGet("/", () => "Real Estate Agency 17");

app.Run();
