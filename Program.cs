using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Interfaces;
using ProductApi.Repository;
using ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.MapControllers();

app.Run();