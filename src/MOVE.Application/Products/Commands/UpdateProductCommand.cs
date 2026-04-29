using MediatR;
using MOVE.Application.Interfaces;

namespace MOVE.Application.Products.Commands;

public record UpdateProductCommand(
	int Id,
	string Name,
	string Description,
	decimal Price,
	int StockQuantity,
	string ImageUrl,
	int CategoryId) : IRequest<bool>;

public class UpdateProductCommandHandler
	: IRequestHandler<UpdateProductCommand, bool>
{
	private readonly IProductRepository _repository;

	public UpdateProductCommandHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<bool> Handle(
		UpdateProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = await _repository.GetByIdAsync(request.Id);

		if (product == null) return false;

		product.Name = request.Name;
		product.Description = request.Description;
		product.Price = request.Price;
		product.StockQuantity = request.StockQuantity;
		product.ImageUrl = request.ImageUrl;
		product.CategoryId = request.CategoryId;

		await _repository.UpdateAsync(product);
		return true;
	}
}