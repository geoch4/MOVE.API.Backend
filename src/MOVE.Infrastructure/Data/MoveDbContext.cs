using Microsoft.EntityFrameworkCore;
using MOVE.Domain.Entities;

namespace MOVE.Infrastructure.Data;

public class MoveDbContext : DbContext
{
	public MoveDbContext(DbContextOptions<MoveDbContext> options)
		: base(options) { }

	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
}