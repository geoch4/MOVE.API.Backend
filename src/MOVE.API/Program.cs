using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MOVE.Application.Behaviours;
using MOVE.Domain.Interfaces;
using MOVE.Infrastructure.Data;
using MOVE.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<MoveDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// MediatR + Pipeline Behaviour
builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssembly(
		typeof(MOVE.Application.Products.Queries.GetAllProductsQuery).Assembly);
	cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

// FluentValidation
builder.Services.AddValidatorsFromAssembly(
	typeof(MOVE.Application.Products.Commands.CreateProductCommandValidator).Assembly);

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
	cfg.AddProfile<MOVE.Application.Mappings.MappingProfile>());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();