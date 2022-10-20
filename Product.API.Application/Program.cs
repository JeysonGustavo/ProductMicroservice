using Microsoft.EntityFrameworkCore;
using Product.API.Core.Infrastructure;
using Product.API.Core.Manager;
using Product.API.Infrastructure.DAL;
using Product.API.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("Microservices"));

builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IProductDAL, ProductDAL>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

InitializeDb.Initialize(app);

app.Run();
