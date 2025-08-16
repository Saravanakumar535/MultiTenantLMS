using Microsoft.EntityFrameworkCore;
using MultiTenantLMS.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using MultiTenantLMS.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 33))));

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MultiTenant LMS v1"));
}

// Other middleware
app.UseHttpsRedirection();
app.UseRouting();
app.UseTenantMiddleware();

app.UseAuthorization();
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
