namespace MOVE.Domain.Entities;

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public int StockQuantity { get; set; }
	public string ImageUrl { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	// Foreign key - kopplingen till Category
	public int CategoryId { get; set; }

	// Navigation property
	public Category Category { get; set; } = null!;
}