using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Application.Features.User.Commands;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Infrastructure.Data;
using PharmacyManagementSystem.Infrastructure.Repositories;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Register Infrastructure Layer (DbContext & Repository Pattern)

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<ISalesItemsRepository, SalesItemsRepository>();
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register MedaitR

// Option A: Use AssemblyReference (after making it public)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});

// Option B: Use any handler type directly
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
});

// Option C: Use the Assembly name directly
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
});
// Register JWT

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Register Swagger Generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register JWT Authentication (Placeholder)
// builder.Services.AddAuthentication(...)

var app = builder.Build();

// ==========================================
// 4. HTTP Request Pipeline
// ==========================================

if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharmacy Management API V1");
    });
}

app.UseHttpsRedirection();

// Add Authentication Middleware if implemented
// app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
