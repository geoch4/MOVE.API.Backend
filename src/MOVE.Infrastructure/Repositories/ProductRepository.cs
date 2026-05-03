using Microsoft.EntityFrameworkCore;
using MOVE.Application.Interfaces;
using MOVE.Domain.Entities;
using MOVE.Infrastructure.Data;

namespace MOVE.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly MoveDbContext _context;

	public ProductRepository(MoveDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Product>> GetAllAsync()
		=> await _context.Products.Include(p => p.Category).ToListAsync();

	public async Task<Product?> GetByIdAsync(int id)
		=> await _context.Products.Include(p => p.Category)
			.FirstOrDefaultAsync(p => p.Id == id);

	public async Task AddAsync(Product product)
	{
		await _context.Products.AddAsync(product);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Product product)
	{
		_context.Products.Update(product);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var product = await GetByIdAsync(id);
		if (product != null)
		{
			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
		}
	}
}