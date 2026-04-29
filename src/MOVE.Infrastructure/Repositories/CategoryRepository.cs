using Microsoft.EntityFrameworkCore;
using MOVE.Application.Interfaces;
using MOVE.Domain.Entities;
using MOVE.Domain.Interfaces;
using MOVE.Infrastructure.Data;

namespace MOVE.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
	private readonly MoveDbContext _context;

	public CategoryRepository(MoveDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Category>> GetAllAsync()
		=> await _context.Categories.Include(c => c.Products).ToListAsync();

	public async Task<Category?> GetByIdAsync(int id)
		=> await _context.Categories.Include(c => c.Products)
			.FirstOrDefaultAsync(c => c.Id == id);

	public async Task AddAsync(Category category)
	{
		await _context.Categories.AddAsync(category);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Category category)
	{
		_context.Categories.Update(category);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var category = await GetByIdAsync(id);
		if (category != null)
		{
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
		}
	}
}