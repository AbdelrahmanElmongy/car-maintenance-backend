using Microsoft.EntityFrameworkCore;
using CarMaintenance.Data;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Car Maintenance API",
        Version = "v1",
        Description = "API for the Car Maintenance management system — includes admin dashboard, notifications, order search, and test items."
    });
    options.EnableAnnotations();
});

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware order مهم جدًا 👇
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");   // 👈 هنا قبل Authorization

app.UseAuthorization();

app.MapControllers();

app.Run();