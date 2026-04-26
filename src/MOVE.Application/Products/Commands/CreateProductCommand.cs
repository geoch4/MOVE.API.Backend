using MediatR;
using MOVE.Domain.Entities;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Products.Commands;

public record CreateProductCommand(
	string Name,
	string Description,
	decimal Price,
	int StockQuantity,
	string ImageUrl,
	int CategoryId) : IRequest<Product>;

public class CreateProductCommandHandler
	: IRequestHandler<CreateProductCommand, Product>
{
	private readonly IProductRepository _repository;

	public CreateProductCommandHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<Product> Handle(
		CreateProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = new Product
		{
			Name = request.Name,
			Description = request.Description,
			Price = request.Price,
			StockQuantity = request.StockQuantity,
			ImageUrl = request.ImageUrl,
			CategoryId = request.CategoryId,
			CreatedAt = DateTime.UtcNow
		};

		await _repository.AddAsync(product);
		return product;
	}
}